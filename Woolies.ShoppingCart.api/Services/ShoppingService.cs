using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Woolies.Shopping.api.Client;
using Woolies.Shopping.Interface;
using Woolies.Shopping.Interface.Response;
using Woolies.Shopping.Models;

namespace Woolies.Shopping.api.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly IWoolieClient woolieClient;
        private readonly IConfiguration config;
        private readonly string TokenId;
        private Config configRead;
        public ShoppingService(IConfiguration configuration)
        {
            this.config = configuration;
            configRead = new Config(configuration);
        }
        public ShoppingService()
        {

        }
        public ShoppingService(IWoolieClient woolieClient, IConfiguration configuration)
        {
            this.config = configuration;
            configRead = new Config(configuration);
            this.woolieClient = woolieClient;
        }

        public UserResponse GetUser()
        {

            UserResponse User = new UserResponse();
            User.Name = configRead.Name;
            User.Token = configRead.TokenId;
            return User;
        }

        public async Task<(List<Product>, ModelStateDictionary)> GetSortProduct(string option)
        {

            SORTOPTIONS sortOption;
            if (string.IsNullOrWhiteSpace(option))
            {
                sortOption = SORTOPTIONS.NONE;
            }
            else if (!Enum.TryParse(option, true, out sortOption))
            {
                var errors = new ModelStateDictionary();
                errors.AddModelError("Sort", "Not a valid sort option found");
                return (null, errors);
            }

            List<Product> sortedProducts = new List<Product>();
            List<Product> products = new List<Product>();
            string resource = string.Format(configRead.ProductResource, configRead.TokenId);
            string jsonContent = await woolieClient.GetData(resource);
            products = JsonConvert.DeserializeObject<List<Product>>(jsonContent);
            if (sortOption == SORTOPTIONS.RECOMMENDED)
            {
                resource = string.Format(configRead.ShopperResource, configRead.TokenId);
                jsonContent = await woolieClient.GetData(resource);
                List<ShopperHistory> shopperHistories = JsonConvert.DeserializeObject<List<ShopperHistory>>(jsonContent);

                // Summing up the totaly quantity for each product from shopper histories to order the top selling product
                var productsDict = new Dictionary<string, Product>();
                foreach (ShopperHistory shopperHistory in shopperHistories)
                {
                    foreach (Product product in shopperHistory.products)
                    {
                        if (productsDict.ContainsKey(product.Name))
                        {
                            productsDict[product.Name].Quantity += product.Quantity;
                        }
                        else
                        {
                            productsDict.Add(product.Name, product);
                        }
                    }
                }

                // Extracting the popular product from product list based on the sorting of quantity
                var popularProduct = productsDict.Values.ToList().OrderByDescending(p => p.Quantity).Join(products,
                                                prodDict => prodDict.Name,
                                                product => product.Name,
                                                (prodDict, product) => product);

                // Including the remaining product list from the product api
                sortedProducts = popularProduct.Union(products).ToList();

            }
            else
            {
                switch (sortOption)
                {
                    case SORTOPTIONS.LOW:
                        {
                            sortedProducts = products.OrderBy(x => x.Price).ToList<Product>();
                            break;
                        }
                    case SORTOPTIONS.HIGH:
                        {
                            sortedProducts = products.OrderByDescending(x => x.Price).ToList<Product>();
                            break;
                        }
                    case SORTOPTIONS.ASCENDING:
                        {
                            sortedProducts = products.OrderBy(x => x.Name).ToList<Product>();
                            break;
                        }
                    case SORTOPTIONS.DESCENDING:
                        {
                            sortedProducts = products.OrderByDescending(x => x.Name).ToList<Product>();
                            break;
                        }
                    default:
                        {
                            sortedProducts = products;
                            break;
                        }
                }
            }

            return (sortedProducts, null);
        }

        public async Task<decimal> GetLowestPossibleTotal(TrolleyCalculatorRequest request)
        {
            string resource = string.Format(configRead.TrolleyResource, configRead.TokenId);
            string jsonContent = await woolieClient.PostData<TrolleyCalculatorRequest>(resource, request);
            return decimal.Parse(jsonContent);
        }
    }
}
