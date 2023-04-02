using cryptocurrency_viewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Services.CryptoData
{
    interface ICryptoDataService
    {
        public Task<List<Asset>> GetAssetsAsync(int limit = 10);
    }
}
