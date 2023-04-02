using cryptocurrency_viewer.Controllers;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace cryptocurrency_viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICryptoView
    {
        private readonly CryptoController _cryptoController;

        private List<Asset>? _lastAssets;

        public MainWindow()
        {
            InitializeComponent();

            _cryptoController = new CryptoController(this, new CryptoDataService());
        }

        public async Task UpdateTableDataAsync(List<Asset> newAssets)
        {
            Dispatcher.Invoke(() =>
            {
                currencyDataGrid.ItemsSource = newAssets;
            });

            if (_lastAssets != null)
            {
                await BlinkChangedRowsAsync(newAssets);
            }
            _lastAssets = newAssets;
        }

        public void BlinkTableRow(DataGridRow row, Color color)
        {
            var animation = new ColorAnimation
            {
                From = Colors.White,
                To = color,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(1)
            };
            var brush = new SolidColorBrush(Colors.White);
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            row.Background = brush;
        }

        public async Task BlinkChangedRowsAsync(List<Asset> newAssets)
        {
            foreach (var newAsset in newAssets)
            {
                Asset? oldAsset =
                    _lastAssets!.FirstOrDefault(x => x.id == newAsset.id);

                //check if price has changed
                if (oldAsset != null && oldAsset.priceUsd != newAsset.priceUsd)
                {
                    //if it has doing blink animation after render
                    var row = currencyDataGrid.ItemContainerGenerator
                        .ContainerFromItem(newAsset) as DataGridRow;
                    await Dispatcher.InvokeAsync(() =>
                    {
                        if (row != null)
                        {
                            Color color = newAsset.priceUsd > oldAsset.priceUsd
                                ? Colors.LightGreen
                                : Colors.Red;
                            BlinkTableRow(row, color);
                        }
                    }, DispatcherPriority.Render);
                }
            }
        }
    }
}
