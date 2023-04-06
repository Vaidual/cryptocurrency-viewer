using cryptocurrency_viewer.Controllers;
using cryptocurrency_viewer.EventArguments;
using cryptocurrency_viewer.Models;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Services.NavigationService;
using cryptocurrency_viewer.ViewModels;
using cryptocurrency_viewer.Views;
using Microsoft.Extensions.DependencyInjection;
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
        public MainWindow()
        {
            InitializeComponent();

            DataContext = (Application.Current as App).ServiceProvider.GetService<MainVM>();
            (Application.Current as App).ServiceProvider.GetService<INavigationService>().NavigateTo<CryptoTableVM>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox)!.Text;
            (DataContext as MainVM)?.TextChanged(text);
        }
    }
}
