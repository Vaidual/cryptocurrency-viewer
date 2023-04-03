using cryptocurrency_viewer.Controllers;
using cryptocurrency_viewer.EventArguments;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Views.CryptoDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace cryptocurrency_viewer.Views.CryptoTable
{
    /// <summary>
    /// Interaction logic for CryptoTablePage.xaml
    /// </summary>
    public partial class CryptoTablePage : Page, ICryptoTableView
    {
        private readonly CryptoViewModel _viewModel;

        public CryptoTablePage()
        {
            InitializeComponent();

            _viewModel = new CryptoViewModel(new CryptoDataService());
            _viewModel.AssetPriceChanged += AssetPriceChangedAsync;
            DataContext = _viewModel;
        }

        public async void BlinkTableRowAsync(DataGridRow row, Color color)
        {
            var brush = new SolidColorBrush(Colors.White);

            var animation = new ColorAnimation
            {
                From = Colors.White,
                To = color,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(1)
            };

            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            row.Background = brush;
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

            CryptoDetailPage detailPage = new CryptoDetailPage(selectedAsset);

            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(detailPage);
        }
    }
}
