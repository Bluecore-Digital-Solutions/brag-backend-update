using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Brag.SharedServices.Helpers.HttpClientHelper
{
    public interface IHttpClientService
    {
        Task<Tout> PostAsync<Tin, Tout>(Tin data, string requestUrl, Dictionary<string, string> headers = null);

        Task<Tout> GetAsync<Tout>(string requestUrl, Dictionary<string, string> headers);
    }
}
