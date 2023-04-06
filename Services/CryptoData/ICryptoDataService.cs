using cryptocurrency_viewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Services.CryptoData
{
    public interface ICryptoDataService
    {
        public Task<Response<List<Asset>>> GetAssetsAsync(int limit = 10, string? searchKey = null);
        public Task<Response<List<AssetPriceHistory>>> GetAssetPriceHistoryAsync(string asssetId, string interval, DateTime start, DateTime? end = null);
        public Task<Response<List<Market>>> GetAssetMarket(string assetId, int limit = 100);
        public Task<Response<Exchange>> GetExchange(string id);
    }
}
