using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;
using Woolies.Shopping.Interface;
using Woolies.Shopping.Interface.Response;
using Woolies.Shopping.Models;

namespace Woolies.Shopping.api.Services
{
    public interface IShoppingService
    {
        Task<decimal> GetLowestPossibleTotal(TrolleyCalculatorRequest request);
        Task<(List<Product>, ModelStateDictionary)> GetSortProduct(string option);
        UserResponse GetUser();
    }
}