using cryptocurrency_viewer.Controllers;
using cryptocurrency_viewer.EventArguments;
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
        private readonly CryptoViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new CryptoViewModel(new CryptoDataService());
            _viewModel.AssetPriceChanged += AssetPriceChangedAsync;
            DataContext = _viewModel;
        }

        public async void BlinkTableRowAsync(DataGridRow row, Color color)
        {
            var brush = new SolidColorBrush(Colors.White);
            row.Background = brush;

            var animation = new ColorAnimation
            {
                From = Colors.White,
                To = color,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(1)
            };

            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            await Task.Delay(animation.Duration.TimeSpan);
            row.Background = Brushes.Transparent;
        }

        private async void AssetPriceChangedAsync(object sender, ChangedAsset e)
        {
            var item = currencyDataGrid.ItemContainerGenerator.Items.First(i => e.Id == (i as Asset).id);
            var row = currencyDataGrid.ItemContainerGenerator
                .ContainerFromItem(item) as DataGridRow;

            await Dispatcher.InvokeAsync(new Action(() =>
            {
                if (row != null)
                {
                    Color color = e.IsPriceHigher
                        ? Colors.LightGreen
                        : Colors.Red;
                    BlinkTableRowAsync(row, color);
                }
            }), DispatcherPriority.Render);
        }

        private void currencyDataGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Asset selectedAsset = (Asset)currencyDataGrid.SelectedItem;

            CryptoDetailUserControl detailUserControl = new CryptoDetailUserControl(selectedAsset);

            this.Content = detailUserControl;
        }
    }
}
