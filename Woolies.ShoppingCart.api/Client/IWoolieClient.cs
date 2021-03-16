using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Woolies.Shopping.api.Client
{
    public interface IWoolieClient
    {
        Task<string> GetData(string requestPath);
        Task<string> PostData<T>(string requestPath, T postData);
    }
}
