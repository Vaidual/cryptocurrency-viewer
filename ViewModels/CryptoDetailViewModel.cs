using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.StaticClasses;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using System.Windows.Input;
using cryptocurrency_viewer.Utilities;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Newtonsoft.Json.Linq;
using System.Windows;
using LiveChartsCore.Drawing;

namespace cryptocurrency_viewer.ViewModels
{
    public class CryptoDetailViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoDataService _cryptoService;

        private Asset _asset;

        private readonly ObservableCollection<AssetPriceHistory> _observableValues;
        public ObservableCollection<ISeries> Series { get; set; }

        public Asset Asset
        {
            get { return _asset; }
            set
            {
                if (_asset != value)
                {
                    _asset = value;
                    OnPropertyChanged(nameof(Asset));
                }
            }
        }

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Labeler = value => TimeConverter.UnixToDateTime((long)value).ToString("htt"),
                UnitWidth = TimeSpan.FromMinutes(5).Ticks,
                MinStep = TimeSpan.FromHours(1).Ticks,
                LabelsRotation = -45,
                LabelsPaint = new SolidColorPaint(SKColors.Black, 1),
            }
        };

        public Axis[] YAxes { get; set; } =
{
            new Axis
            {
                Labeler = value => $"{value:N2}",
            }
        };

        public CryptoDetailViewModel(Asset asset, ICryptoDataService cryptoService)
        {
            _cryptoService= cryptoService;

            Asset = asset;

            _observableValues = new ObservableCollection<AssetPriceHistory>();
            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<AssetPriceHistory>
                {
                    Values = _observableValues,
                    Fill = new SolidColorPaint(SKColor.FromHsl(149, 100, 88)),
                    GeometrySize = 0,
                    Stroke = new SolidColorPaint(SKColor.FromHsl(148, 71, 61), 2),
                    Name = null,
                    TooltipLabelFormatter =
                        (chartPoint) => $"{TimeConverter.UnixToDateTime((long)chartPoint.SecondaryValue).ToString("MMM dd, yyyy - h:mmtt")}" +
                        $"{Environment.NewLine}{chartPoint.PrimaryValue:N2}",
                    Mapping = (assetPriceHistory, point) =>
                    {
                        point.PrimaryValue = assetPriceHistory.PriceUsd;
                        point.SecondaryValue = assetPriceHistory.UnixTimestamp;
                    }
                }
            };

            InitiallizeChart();
        }

        private async void InitiallizeChart()
        {
            await RedrawChart();
        }

        private async Task RedrawChart()
        {
            var response = await _cryptoService.GetAssetPriceHistoryAsync(Asset.Id, TimeInterval.Mimutes5, DateTime.Now.AddDays(-1));
            if (!response.IsSuccess)
            {
                return;
            }
            foreach (var e in response.Data)
            {
                _observableValues.Add(e);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
