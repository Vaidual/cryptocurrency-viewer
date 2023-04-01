using cryptocurrency_viewer.Models;
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
    public partial class MainWindow : Window
    {
        List<Asset>? lastAssets;
        private readonly Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            
            //data on api refreshes ~ every 20 seconds.
            timer = new Timer(
                UpdateTableAsync, 
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromSeconds(20)
            );
        }

        private async void UpdateTableAsync(object? state)
        {
            List<Asset>? newAssets = await GetCryptoDataAsync();
            if (newAssets == null)
            {
                return;
            }

            //updating table
            Dispatcher.Invoke(() =>
            {
                currencyDG.ItemsSource = newAssets;
            });

            if (lastAssets != null)
            {
                foreach (var newAsset in newAssets)
                {
                    Asset? oldAsset = 
                        lastAssets.FirstOrDefault(x => x.id == newAsset.id);
                    //check if price has changed
                    if (oldAsset != null && oldAsset.priceUsd != newAsset.priceUsd)
                    {
                        //if it has doing blink animation after render
                        await Dispatcher.InvokeAsync(() =>
                        {
                            var row = currencyDG.ItemContainerGenerator
                                .ContainerFromItem(newAsset) as DataGridRow;
                            if (row != null)
                            {
                                Color color = newAsset.priceUsd > oldAsset.priceUsd
                                    ? Colors.LightGreen
                                    : Colors.Red; 
                                MakeRowBlink(row, color);
                            }
                        }, DispatcherPriority.Render);
                    }
                }
            }
            lastAssets = newAssets;
        }

        private async Task<List<Asset>?> GetCryptoDataAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer", 
                    "d0748243-b475-4ac1-9fda-7ebaeb98787f"
                );
            var uri = new Uri("http://api.coincap.io/v2/assets?limit=10");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(jsonString);
            return jObject.GetValue("data")!.ToObject<List<Asset>>()!;
        }

        private void MakeRowBlink(DataGridRow row, Color color)
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
    }
}
