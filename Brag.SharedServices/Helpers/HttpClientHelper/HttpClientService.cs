using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Brag.SharedServices.Helpers.HttpClientHelper
{
    public class HttpClientService : IHttpClientService
    {
        private HttpClient _httpClient => new HttpClient();
        public async Task<Tout> PostAsync<Tin, Tout>(Tin data, string requestUrl, Dictionary<string, string> headers = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(requestMessage);
            var responseJsonString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                responseJsonString = await response.Content.ReadAsStringAsync();

                try
                {
                    var result = JsonConvert.DeserializeObject<Tout>(responseJsonString);
                    return result!;
                }
                catch (Exception ex)
                {

                    throw new Exception($"An error occurred while deserializing httpResponse for {data}. Exception: {ex.Message}");
                }
            }
            throw new Exception($"An error occurred while sending request for {data}");
        }


        public async Task<Tout> GetAsync<Tout>(string requestUrl, Dictionary<string, string> headers)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            var response = await _httpClient.SendAsync(requestMessage);
            var responseJsonString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                responseJsonString = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<Tout>(responseJsonString);
                    return result!;
                }
                catch (Exception ex)
                {

                    throw new Exception($"An error occurred while deserializing httpResponse for {requestUrl}. Exception: {ex.Message}");
                }
            }
            throw new Exception($"An error occurred while sending request for {requestUrl}");
        }
    }
}
