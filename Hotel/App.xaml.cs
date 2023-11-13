using System.IO;
using System.Windows;
using Hotel.Factories;
using Hotel.MVVM.ViewModels;
using Hotel.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hotel.Infrastructure.Extensions;
using Hotel.Services;
using Hotel.Services.Interfaces;


namespace Hotel;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private readonly IHost _host;

    public App()
    {
        var connectionString = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("HotelDbContext");
        
        _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddInfrastructure(connectionString);
                
                // Services
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<INavigator, Navigator>();
                
                //ViewModels
                services.AddTransient<ReservationsListingViewModel>();
                services.AddTransient<TestViewModel>();
                
                services.AddSingleton<CreateViewModel<ReservationsListingViewModel>>(services => services.GetRequiredService<ReservationsListingViewModel>);
                services.AddSingleton<CreateViewModel<TestViewModel>>(services => services.GetRequiredService<TestViewModel>);
                
                
                services.AddSingleton<NavigationModalViewStore>();
                
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton(s => new MainWindow
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });
                
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.StopAsync();
        _host.Dispose();
        
        base.OnExit(e);
    }
}