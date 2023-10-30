using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
                {
                    services.AddSingleton<INavigationService, Services.NavigationService>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var navigationStore = _host.Services.GetRequiredService<NavigationStore>();
            navigationStore.CurrentViewModel = new AccountsListingViewModel();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            
            base.OnStartup(e);
        }
    }
}