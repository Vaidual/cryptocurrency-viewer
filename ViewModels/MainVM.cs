using cryptocurrency_viewer.CustomCommads;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Services.NavigationService;
using cryptocurrency_viewer.Utilities;
using cryptocurrency_viewer.Views;
using Microsoft.Extensions.DependencyInjection;
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
    public class MainVM : ViewModelBase
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
        public MainVM(INavigationService navigationService, ICryptoDataService cryptoDataService) : base(navigationService)
        {
            NavigateToAssetDetailsPageCommand = new RelayCommand(NavigateToAssetDetailsPage, (data) => true);
            GoBackCommand = new RelayCommand(GoBack, (data) => true);
            GoForwardCommand = new RelayCommand(GoForward, (data) => true);

            this._cryptoDataService = cryptoDataService;
        }

        private async Task ExecuteSearchAsync()
        {
            if (SearchQuery == String.Empty)
            {
                return;
            }
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
                SearchQuery = string.Empty;
                SearchResults = new ObservableCollection<Asset>();
                IsListBoxVisible = false;
                _navigationService.NavigateTo<CryptoDetailVM>(selectedAsset);
            }
        }

        public RelayCommand GoBackCommand { get; set; }
        private void GoBack(object selectedItem)
        {
            _navigationService.GoBack();
        }

        public RelayCommand GoForwardCommand { get; set; }
        private void GoForward(object selectedItem)
        {

            _navigationService.GoForward();
        }

    }
}
