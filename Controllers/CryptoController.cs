using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace cryptocurrency_viewer.Controllers
{
    class CryptoController
    {
        private readonly ICryptoView _cryptoView;
        private readonly ICryptoDataService _cryptoService;

        //data on api refreshes ~ every 20 seconds.
        const float refreshRate = 20;

        private readonly Timer _timer;

        public CryptoController(ICryptoView view, ICryptoDataService model)
        {
            _cryptoView = view;
            _cryptoService = model;

            _timer = new Timer(
                UpdateTableAsync,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(refreshRate)
            );
        }

        private async void UpdateTableAsync(object? state)
        {
            List<Asset> newAssets = await _cryptoService.GetAssetsAsync();
            await _cryptoView.UpdateTableDataAsync(newAssets);
        }
    }
}
