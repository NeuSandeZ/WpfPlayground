using System.IO;
using System.Windows;
using Hotel.Application.Extensions;
using Hotel.Factories;
using Hotel.Infrastructure.Extensions;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services;
using Hotel.Services.Interfaces;
using Hotel.Stores;
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
                services.AddSingleton<MessengerCurrentViewStorage>();

                //ViewModels

                services.AddTransient<ReservationsListingViewModel>();
                services.AddTransient<GuestViewModel>();
                services.AddTransient<RoomsViewModel>();
                services.AddTransient<AddGuestViewModel>();
                services.AddTransient<AddReservationViewModel>();
                services.AddTransient<PaymentViewModel>();
                services.AddTransient<RoomsViewModel>();
                services.AddTransient<AddRoomViewModel>();
                services.AddTransient<CheckInsOutsViewModel>();
                services.AddTransient<StaffViewModel>();
                services.AddTransient<TasksViewModel>();
                services.AddTransient<AddStaffViewModel>();
                services.AddTransient<AddTaskViewModel>();

                //ViewModels Factory

                services.AddSingleton<CreateViewModel<ReservationsListingViewModel>>(services =>
                    services.GetRequiredService<ReservationsListingViewModel>);
                services.AddSingleton<CreateViewModel<GuestViewModel>>(services =>
                    services.GetRequiredService<GuestViewModel>);
                services.AddSingleton<CreateViewModel<RoomsViewModel>>(services =>
                    services.GetRequiredService<RoomsViewModel>);
                services.AddSingleton<CreateViewModel<AddGuestViewModel>>(services =>
                    services.GetRequiredService<AddGuestViewModel>);
                services.AddSingleton<CreateViewModel<AddReservationViewModel>>(services =>
                    services.GetRequiredService<AddReservationViewModel>);
                services.AddSingleton<CreateViewModel<AddRoomViewModel>>(services =>
                    services.GetRequiredService<AddRoomViewModel>);
                services.AddSingleton<CreateViewModel<CheckInsOutsViewModel>>(services =>
                    services.GetRequiredService<CheckInsOutsViewModel>);
                services.AddSingleton<CreateViewModel<PaymentViewModel>>(services =>
                    services.GetRequiredService<PaymentViewModel>);
                services.AddSingleton<CreateViewModel<StaffViewModel>>(services =>
                    services.GetRequiredService<StaffViewModel>);
                services.AddSingleton<CreateViewModel<TasksViewModel>>(services =>
                    services.GetRequiredService<TasksViewModel>);
                services.AddSingleton<CreateViewModel<AddStaffViewModel>>(services =>
                    services.GetRequiredService<AddStaffViewModel>);
                services.AddSingleton<CreateViewModel<AddTaskViewModel>>(services =>
                    services.GetRequiredService<AddTaskViewModel>);


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