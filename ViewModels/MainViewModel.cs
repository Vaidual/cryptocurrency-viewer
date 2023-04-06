using cryptocurrency_viewer.CustomCommads;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Views.CryptoDetail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace cryptocurrency_viewer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        private ObservableCollection<Asset> _searchResults;
        private ICryptoDataService _cryptoDataService;

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                ExecuteSearchAsync();
            }
        }

        public ObservableCollection<Asset> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        private bool _isListBoxVisible = false;
        public bool IsListBoxVisible
        {
            get => _isListBoxVisible;
            set
            {
                _isListBoxVisible = value;
                OnPropertyChanged(nameof(IsListBoxVisible));
            }
        }
        public MainViewModel(ICryptoDataService cryptoDataService)
        {
            NavigateToAssetDetailsPageCommand = new RelayCommand(NavigateToAssetDetailsPage, (data) => true);
            this._cryptoDataService = cryptoDataService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ExecuteSearchAsync()
        {
            // Implement your search algorithm here
            var assets = await _cryptoDataService.GetAssetsAsync(6, SearchQuery);
            var newSearchResult = assets.Data.Select(a => $"{a.Name} ({a.Symbol})");
            SearchResults = new ObservableCollection<Asset>(assets.Data);
            IsListBoxVisible = SearchResults.Count > 0;
        }

        public void TextChanged(string newText)
        {
            SearchQuery = newText;
        }

        public RelayCommand NavigateToAssetDetailsPageCommand { get; set; }
        private void NavigateToAssetDetailsPage(object selectedItem)
        {
            if (selectedItem is Asset selectedAsset)
            {
                var cryptoDetailViewModel = new CryptoDetailViewModel(selectedAsset, _cryptoDataService);

                CryptoDetailPage detailPage = new CryptoDetailPage();
                detailPage.DataContext = cryptoDetailViewModel;

                //this.NavigationService.Navigate(detailPage);
                // Navigate to the details page, passing the selected asset as a parameter
                //_navigationService.NavigateTo("DetailsPage", asset);
            }
        }

    }
}
