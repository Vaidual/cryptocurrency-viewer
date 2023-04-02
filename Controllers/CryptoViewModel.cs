﻿using cryptocurrency_viewer.EventArguments;
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
    class CryptoViewModel : INotifyPropertyChanged
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

        private readonly Timer _timer;

        public CryptoViewModel(ICryptoDataService model)
        {
            _cryptoService = model;

            _assets = new List<Asset>();
            PreviousAssets = new List<Asset>();

            _timer = new Timer(
                new TimerCallback(UpdateTableAsync),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5)
            );
        }

        private async void UpdateTableAsync(object? state)
        {
            PreviousAssets = Assets.ToList();
            Assets = await _cryptoService.GetAssetsAsync();
            NotifyChangedRows();
        }

        public event EventHandler<ChangedAsset> AssetPriceChanged;

        private void NotifyChangedRows()
        {
            foreach (var newAsset in Assets)
            {
                Asset? prevAsset =
                    PreviousAssets.FirstOrDefault(x => x.id == newAsset.id);

                //check if price has changed
                if (prevAsset != null && prevAsset.priceUsd != newAsset.priceUsd)
                {
                    var arg = new ChangedAsset(newAsset.id, newAsset.priceUsd > prevAsset.priceUsd);
                    AssetPriceChanged.Invoke(this, arg);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
