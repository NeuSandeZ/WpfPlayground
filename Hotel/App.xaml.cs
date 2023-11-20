using System.IO;
using System.Windows;
using Hotel.Application.Extensions;
using Hotel.Application.ReservationListingDto;
using Hotel.Factories;
using Hotel.Infrastructure.Extensions;
using Hotel.MVVM.ViewModels;
using Hotel.Services;
using Hotel.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                services.AddApplication();
                
                // Services
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<INavigator, Navigator>();
                
                // Models 

                services.AddTransient<ReservationDto>();


                //ViewModels
                
                services.AddTransient<ReservationsListingViewModel>();
                services.AddTransient<TestViewModel>();
                services.AddTransient<TextXDViewModel>();
                services.AddTransient<AddReservationViewModel>();
                
                //ViewModels Factory

                services.AddSingleton<CreateViewModel<ReservationsListingViewModel>>(services =>
                    services.GetRequiredService<ReservationsListingViewModel>);
                services.AddSingleton<CreateViewModel<TestViewModel>>(services =>
                    services.GetRequiredService<TestViewModel>);
                services.AddSingleton<CreateViewModel<TextXDViewModel>>(services =>
                    services.GetRequiredService<TextXDViewModel>);
                services.AddSingleton<CreateViewModel<AddReservationViewModel>>(services =>
                    services.GetRequiredService<AddReservationViewModel>);
                

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