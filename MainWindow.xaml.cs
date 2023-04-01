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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
            
            timer = new Timer(UpdateTableAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }

        private async void UpdateTableAsync(object? state)
        {
            List<Asset> newAssets = await GetCryptoDataAsync();
            if (newAssets.Count == 0)
            {
                return;
            }
            Dispatcher.Invoke( () =>
            {
                currencyDG.ItemsSource = newAssets;
            });
            if (lastAssets == null)
            {
                return;
            }
            for (int i = 0; i < newAssets.Count; i++)
            {
                if (newAssets[i] == lastAssets[i])
                {
                }
            }
        }

        private async Task<List<Asset>> GetCryptoDataAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "d0748243-b475-4ac1-9fda-7ebaeb98787f");
            var uri = new Uri("http://api.coincap.io/v2/assets?limit=10");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<Asset>();
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(jsonString);
            return jObject.GetValue("data")!.ToObject<List<Asset>>()!;
        }
    }
}
