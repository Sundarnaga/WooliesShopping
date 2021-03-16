using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Woolies.Shopping.api
{
    public class Config
    {
        private readonly IConfiguration _configuration;
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Name { get { return _configuration.GetValue<string>(CONSTANTS.CONFIG_NAME); } }

        public string WooliesClientUrl { get { return _configuration.GetValue<string>(CONSTANTS.CONFIG_WOOLIESCLIENTURL); } }

        public string TokenId { get { return _configuration.GetValue<string>(CONSTANTS.CONFIG_TOKENID); } }

        public string ShopperResource { get { return _configuration.GetValue<string>(CONSTANTS.CONFIG_SHOPPERESOURCE); } }

        public string ProductResource { get { return _configuration.GetValue<string>(CONSTANTS.CONFIG_PRODUCTRESOURCE); } }

        public string TrolleyResource { get { return _configuration.GetValue<string>(CONSTANTS.CONFIG_TROLLEYRESOURCE); } }
    }
}
