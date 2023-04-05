using cryptocurrency_viewer.Controllers;
using cryptocurrency_viewer.Services.CryptoData;
using cryptocurrency_viewer.ViewModels;
using cryptocurrency_viewer.Views.CryptoTable;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace cryptocurrency_viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly ServiceProvider ServiceProvider;

        public App()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICryptoDataService, CryptoDataService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
