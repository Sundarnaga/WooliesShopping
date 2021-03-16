using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Woolies.Shopping.api.Client;

namespace Woolies.Shopping.api.Client
{
    public class WoolieClient : IWoolieClient
    {
        private readonly HttpClient _client;


        public WoolieClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = httpClient;
        }

        public async Task<string> GetData(string requestPath)
        {
            return await _client.GetStringAsync(requestPath);
        }

        public async Task<string> PostData<T>(string requestPath, T postData)
        {
            var jsonData = JsonConvert.SerializeObject(postData, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(requestPath, httpContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
