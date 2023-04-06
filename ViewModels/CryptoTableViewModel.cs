using cryptocurrency_viewer.EventArguments;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace cryptocurrency_viewer.Controllers
{
    class CryptoTableViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoDataService _cryptoService;

        public List<Asset> PreviousAssets { get; private set; }

        private List<Asset> _assets;
        public List<Asset> Assets
        {
            get { return _assets; }
            private set
            {
                _assets = value;
                OnPropertyChanged(nameof(Assets));

            }
        }

        //data on api refreshes ~ every 20 seconds.
        const float refreshRate = 20;

        private readonly DispatcherTimer _timer;

        public CryptoTableViewModel(ICryptoDataService cryptoDataService)
        {
            _cryptoService = cryptoDataService;

            _assets = new List<Asset>();
            PreviousAssets = new List<Asset>();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(refreshRate);
            _timer.Tick += new EventHandler(UpdateTableAsync);
        }

        private async void UpdateTableAsync(object? sender, EventArgs e)
        {
            PreviousAssets = Assets.ToList();
            var response = await _cryptoService.GetAssetsAsync();
            if (!response.IsSuccess )
            {
                return;
            }
            Assets = response.Data;
            NotifyChangedRows();
        }

        public event EventHandler<ChangedAsset> AssetPriceChanged;

        private void NotifyChangedRows()
        {
            foreach (var newAsset in Assets)
            {
                Asset? prevAsset =
                    PreviousAssets.FirstOrDefault(x => x.Id == newAsset.Id);

                //check if price has changed
                if (prevAsset != null && prevAsset.PriceUsd != newAsset.PriceUsd)
                {
                    var arg = new ChangedAsset(newAsset.Id, newAsset.PriceUsd > prevAsset.PriceUsd);
                    AssetPriceChanged.Invoke(this, arg);
                }
            }
        }

        public void StartTimer()
        {
            UpdateTableAsync(this, EventArgs.Empty);
            _timer.IsEnabled= true;
        }

        public void StopTimer()
        {
            _timer.IsEnabled = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
