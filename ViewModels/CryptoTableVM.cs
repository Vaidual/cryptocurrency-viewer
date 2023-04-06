using cryptocurrency_viewer.CustomCommads;
using cryptocurrency_viewer.EventArguments;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Services.NavigationService;
using cryptocurrency_viewer.Utilities;
using cryptocurrency_viewer.ViewModels;
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
    class CryptoTableVM : ViewModelBase
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

        public CryptoTableVM(INavigationService navigationService, ICryptoDataService cryptoDataService) : base(navigationService)
        {
            _cryptoService = cryptoDataService;

            _assets = new List<Asset>();
            PreviousAssets = new List<Asset>();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(refreshRate);
            _timer.Tick += new EventHandler(UpdateTableAsync);

            NavigateToAssetDetailsPageCommand = new RelayCommand(NavigateToAssetDetailsPage, (data) => true);
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

        public event EventHandler<ChangedAsset>? AssetPriceChanged;

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
                    AssetPriceChanged?.Invoke(this, arg);
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

        public RelayCommand NavigateToAssetDetailsPageCommand { get; set; }
        private void NavigateToAssetDetailsPage(object selectedItem)
        {
            Asset? selectedAsset = selectedItem as Asset;
            if (selectedAsset == null)
            {
                return;
            }

            _navigationService.NavigateTo<CryptoDetailVM>(selectedAsset);
        }
    }
}
