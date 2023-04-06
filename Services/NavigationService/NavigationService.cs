using cryptocurrency_viewer.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace cryptocurrency_viewer.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Stack<(Type, object)> _pageHistory = new Stack<(Type, object)>();
        private int _currentPage = 0;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            var viewModel = _serviceProvider.GetService<TViewModel>();
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            viewModel.Initialize(parameter);

            // Navigate to the view associated with the ViewModel
            Type viewType = ViewLocator.GetViewType(viewModel.GetType());
            FrameworkElement viewElement = Activator.CreateInstance(viewType) as FrameworkElement;
            viewElement.DataContext = viewModel;

            // Show the view on the screen
            ((Application.Current as App).MainWindow as MainWindow).MainFrame.Navigate(viewElement);
            
            if (_currentPage != 0)
            {
                for (int i = 0; i < _currentPage; i++)
                {
                    _pageHistory.Pop();
                }

            }
            _pageHistory.Push((typeof(TViewModel), parameter));
            _currentPage = 0;
        }

        public bool CanGoBack()
        {
            return _currentPage < _pageHistory.Count - 1;
        }

        public void GoBack()
        {
            if (CanGoBack())
            {
                // Get the previous page from the page history stack
                var (viewModelType, parameter) = _pageHistory.ElementAt(_currentPage + 1);
                _currentPage++;

                // Navigate to the previous page
                NavigateTo(viewModelType, parameter);
            }
        }

        public bool CanGoForward()
        {
            return _currentPage > 0;
        }

        public void GoForward()
        {
            if (CanGoForward())
            {
                var (viewModelType, parameter) = _pageHistory.ElementAt(_currentPage - 1);
                _currentPage--;

                NavigateTo(viewModelType, parameter);
            }
        }

        private void NavigateTo(Type viewModelType, object parameter)
        {
            var viewModel = _serviceProvider.GetService(viewModelType);
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            if (viewModel is ViewModelBase viewModelBase)
            {
                viewModelBase.Initialize(parameter);
            }
            (viewModel as ViewModelBase).Initialize(parameter);

            Type viewType = ViewLocator.GetViewType(viewModel.GetType());
            FrameworkElement viewElement = Activator.CreateInstance(viewType) as FrameworkElement;
            viewElement.DataContext = viewModel;

            ((Application.Current as App).MainWindow as MainWindow).MainFrame.Navigate(viewElement);
        }
    }
}
