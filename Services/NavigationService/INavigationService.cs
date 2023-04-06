using cryptocurrency_viewer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Services.NavigationService
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;
        bool CanGoBack();
        void GoBack();
        bool CanGoForward();
        void GoForward();
    }
}
