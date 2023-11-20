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
        
        //TODO check if i have to register GuestMappingProfile as it is in the same assembly
        services.AddAutoMapper(typeof(ReservationMappingProfile), typeof(GuestMappingProfile));
    }
}