using Hotel.Application.Services;
using Hotel.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReservationListingService, ReservationListingService>();
    }
}