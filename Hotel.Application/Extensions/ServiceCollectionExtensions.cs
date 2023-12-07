using Hotel.Application.Mappings;
using Hotel.Application.Services;
using Hotel.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReservationListingService, ReservationListingService>();
        services.AddScoped<IGuestsListingService, GuestsListingService>();
        services.AddScoped<IRoomListingService, RoomListingService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICheckInOutService, CheckInOutService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddAutoMapper(typeof(ReservationMappingProfile),
            typeof(GuestMappingProfile), typeof(RoomMappingProfile),
            typeof(PaymentMappingProfile), typeof(CheckInsOutsMappingProfile),
            typeof(StaffMappingProfile),
            typeof(TaskMappingProfile));
    }
}