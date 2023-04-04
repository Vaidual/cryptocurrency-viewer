using cryptocurrency_viewer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.ViewModels
{
    public class CryptoDetailViewModel : INotifyPropertyChanged
    {
        private Asset _asset;

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

        public CryptoDetailViewModel(Asset asset)
        {
            Asset = asset;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Asset)));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
