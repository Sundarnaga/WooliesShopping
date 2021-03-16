using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Woolies.Shopping.api.Client;
using Woolies.Shopping.api.Services;
using Woolies.Shopping.Infra;
using Woolies.Shopping.Interface;

namespace Woolies.Shopping.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private ILoggerManager logger;
        private readonly IConfiguration config;
        private readonly IWoolieClient woolieClient;
        private ILogger logger1;
        public ShoppingController(ILoggerManager logger, IConfiguration config, IWoolieClient woolieClient)
        {
            this.logger = logger;
            this.config = config;
            this.woolieClient = woolieClient;
        }

        
        /// <summary>
        /// Get the UserName details for supplied token id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User")]
        public IActionResult GetUser()
        {
            ShoppingService service = new ShoppingService(config);
            return Ok(service.GetUser());
        }


        /// <summary>
        /// API to return the products based on sort option in request parameter
        /// </summary>
        /// <param name="sortOption"></param>
        ///  A query string parameter called "sortOption" which will take in the following strings - "Low" - Low to High Price - 
        ///  "High" - High to Low Price - "Ascending" - A - Z sort on the Name - "Descending" - Z - A sort on the Name - "Recommended"
        /// <returns></returns>
        /// <response code="200">Returns a list of products</response>
        /// <response code="400">The request is invalid</response>
        [HttpGet]
        [Route("Sort")]
        public async Task<IActionResult> GetSortProduct([FromQuery]string sortOption)
        {
            ShoppingService service = new ShoppingService(woolieClient, config);
            var (sortedProduct, errors) = await service.GetSortProduct(sortOption);
            if(errors != null)
            {
                return ValidationProblem(errors);
            }
            return Ok(sortedProduct);
        }


        /// <summary>
        /// API to Calculate the Lowest Posisble Total for a given request
        /// </summary>
        /// <param name="request"></param>
        /// Request has list of products, quantities and specials to calculate the lowest possible total
        /// <returns></returns>
        /// <response code="200">Returns a list of products</response>
        /// <response code="400">The request is invalid</response>
        [HttpPost]
        [Route("trolleyTotal")]
        public async Task<IActionResult> GetLowestPossibleTotal([FromBody]TrolleyCalculatorRequest request)
        {
            if (!ModelState.IsValid)
            {
                logger.LogInfo("Invalid Request");
                return ValidationProblem(ModelState);
            }

            ShoppingService service = new ShoppingService(woolieClient, config);
            return Ok(await service.GetLowestPossibleTotal(request));
        }
    }


}