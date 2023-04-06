using cryptocurrency_viewer.Controllers;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.Services.NavigationService;
using cryptocurrency_viewer.ViewModels;
using cryptocurrency_viewer.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace cryptocurrency_viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<ICryptoDataService, CryptoDataService>();
            services.AddTransient<CryptoTableVM>();
            services.AddTransient<CryptoDetailVM>();
            services.AddTransient<MainVM>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
