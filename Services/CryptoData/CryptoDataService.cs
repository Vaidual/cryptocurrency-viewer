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
using System.Configuration;
using Newtonsoft.Json;
using cryptocurrency_viewer.Utilities;
using System.Diagnostics.Eventing.Reader;

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

        public async Task<Response<List<Asset>>> GetAssetsAsync(int limit = 10, string? searchKey = null)
        {
            var response = new Response<List<Asset>>();
            var uri = new Uri(apiUrlBase + $"/assets?limit={limit}" + $"{(searchKey != null ? $"&search={searchKey}" : "")}");
            try
            {
                JToken data = await GetDataFromUri(uri);
                response.Data = data.ToObject<List<Asset>>()!;
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Failed to get data from api.";
            }
            return response;
        }

        public async Task<Response<List<Market>>> GetAssetMarket(string assetId, int limit = 100)
        {
            var response = new Response<List<Market>>();
            var uri = new Uri(apiUrlBase + $"/assets/{assetId}/markets?limit={limit}");
            try
            {
                JToken data = await GetDataFromUri(uri);
                response.Data = data.ToObject<List<Market>>()!;
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Failed to get data from api.";
            }
            return response;
        }

        public async Task<Response<Exchange>> GetExchange(string id)
        {
            var response = new Response<Exchange>();
            var uri = new Uri(apiUrlBase + $"/exchanges/{id}");
            try
            {
                JToken data = await GetDataFromUri(uri);
                response.Data = data.ToObject<Exchange>()!;
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Failed to get data from api.";
            }
            return response;
        }

        public async Task<Response<List<AssetPriceHistory>>> GetAssetPriceHistoryAsync(string asssetId, string interval, DateTime start, DateTime? end = null)
        {
            long endUnix, startUnix = TimeConverter.DateTimeToUnix(start);
            if (end == null)
            {
                endUnix = TimeConverter.GetCurrentUnixTime();
            }
            else
            {
                endUnix = TimeConverter.DateTimeToUnix((DateTime)end);
            }

            var response = new Response<List<AssetPriceHistory>>();
            var uri = new Uri(apiUrlBase + $"/assets/{asssetId}/history?interval={interval}&start={startUnix}&end={endUnix}");
            try
            {
                JToken data = await GetDataFromUri(uri);
                response.Data = data
                    .Select(v => new AssetPriceHistory(
                        v.Value<double>("priceUsd"), 
                        v.Value<long>("time")
                    ))
                .ToList();
            }
            catch(Exception)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Failed to get data from api.";
            }
            return response;
        }

        private async Task<JToken> GetDataFromUri(Uri uri)
        {
            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            JToken data = JObject.Parse(jsonString).GetValue("data")!;

            return data;
        }
    }
}
