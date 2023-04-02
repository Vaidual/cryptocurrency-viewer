using cryptocurrency_viewer.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using cryptocurrency_viewer.Services.CryptoData;

namespace cryptocurrency_viewer
{
    class CryptoDataService : ICryptoDataService
    {
        private readonly HttpClient _client;
        const string apiUrlBase = "http://api.coincap.io/v2";

        public CryptoDataService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", "d0748243-b475-4ac1-9fda-7ebaeb98787f"
            );
        }

        public async Task<List<Asset>> GetAssetsAsync(int limit = 10)
        {
            var uri = new Uri(apiUrlBase + $"/assets?limit={limit}");
            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                var message = "Failed to get assets data from api.";
                throw new Exception(message);
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(jsonString).GetValue("data");
            return data!.ToObject<List<Asset>>()!;
        }
    }
}
