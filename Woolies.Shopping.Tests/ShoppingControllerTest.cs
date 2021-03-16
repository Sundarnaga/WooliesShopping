using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using Woolies.Shopping.api.Client;
using Woolies.Shopping.api.Controllers;
using Woolies.Shopping.Infra;
using System.Net.Http;
using Woolies.Shopping.api;
using Microsoft.AspNetCore.Mvc;
using Woolies.Shopping.Interface.Response;
using System.Collections.Generic;
using Woolies.Shopping.Models;

namespace Woolies.Shopping.Tests
{
    public class ShoppingControllerTest
    {
        private ShoppingController shoppingController;
        private IWoolieClient woolieClient;
        private LoggerManager loggerManager;
        private Config config;
        [SetUp]
        public void Setup()
        {
            loggerManager = new LoggerManager();
            IConfiguration confgration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true)
               .Build();
            config = new Config(confgration);
            woolieClient = new WoolieClient(new HttpClient() { BaseAddress = new System.Uri(config.WooliesClientUrl) });
            shoppingController = new ShoppingController(loggerManager, confgration, woolieClient);
        }

        [Test]
        public void UserNameTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetUser();
            UserResponse userResponse = (UserResponse)actionResult.Value;
            Assert.AreEqual(userResponse.Token, config.TokenId);
        }

        [Test]
        public void UserTokenTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetUser();
            UserResponse userResponse = (UserResponse)actionResult.Value;
            Assert.AreEqual(userResponse.Token, config.TokenId);
        }

        [Test]
        public void SortLowTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetSortProduct("Low").Result;
            List<Product> sortedProducts = (List<Product>)actionResult.Value;
            Product expectedProduct = new Product { Name = "Test Product D", Price = 5.0m, Quantity = 0.0m };
            Assert.AreEqual(expectedProduct.Name, sortedProducts[0].Name);
            Assert.AreEqual(expectedProduct.Price, sortedProducts[0].Price);
            Assert.AreEqual(expectedProduct.Quantity, sortedProducts[0].Quantity);
        }

        [Test]
        public void SortHighTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetSortProduct("High").Result;
            List<Product> sortedProducts = (List<Product>)actionResult.Value;
            Product expectedProduct = new Product { Name = "Test Product F", Price = 999999999999.0m, Quantity = 0.0m };
            Assert.AreEqual(expectedProduct.Name, sortedProducts[0].Name);
            Assert.AreEqual(expectedProduct.Price, sortedProducts[0].Price);
            Assert.AreEqual(expectedProduct.Quantity, sortedProducts[0].Quantity);
        }

        [Test]
        public void SortDescendingTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetSortProduct("Descending").Result;
            List<Product> sortedProducts = (List<Product>)actionResult.Value;
            Product expectedProduct = new Product { Name = "Test Product F", Price = 999999999999.0m, Quantity = 0.0m };
            Assert.AreEqual(expectedProduct.Name, sortedProducts[0].Name);
            Assert.AreEqual(expectedProduct.Price, sortedProducts[0].Price);
            Assert.AreEqual(expectedProduct.Quantity, sortedProducts[0].Quantity);
        }

        [Test]
        public void SortAscendingTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetSortProduct("Ascending").Result;
            List<Product> sortedProducts = (List<Product>)actionResult.Value;
            Product expectedProduct = new Product { Name = "Test Product A", Price = 99.99m, Quantity = 0.0m };
            Assert.AreEqual(expectedProduct.Name, sortedProducts[0].Name);
            Assert.AreEqual(expectedProduct.Price, sortedProducts[0].Price);
            Assert.AreEqual(expectedProduct.Quantity, sortedProducts[0].Quantity);
        }

        [Test]
        public void SortRecommendingTest()
        {
            var actionResult = (OkObjectResult)shoppingController.GetSortProduct("Recommended").Result;
            List<Product> sortedProducts = (List<Product>)actionResult.Value;
            Product expectedProduct = new Product { Name = "Test Product A", Price = 99.99m, Quantity = 0.0m };
            Assert.AreEqual(expectedProduct.Name, sortedProducts[0].Name);
            Assert.AreEqual(expectedProduct.Price, sortedProducts[0].Price);
            Assert.AreEqual(expectedProduct.Quantity, sortedProducts[0].Quantity);
        }

    }
}