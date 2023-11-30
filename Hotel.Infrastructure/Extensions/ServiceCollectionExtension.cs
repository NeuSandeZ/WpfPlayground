using Hotel.Domain.IRepositories;
using Hotel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HotelDbContext>(builder => { builder.UseSqlServer(connectionString); });

        services.AddScoped<IReservationListingRepository, ReservationListingRepository>();
        services.AddScoped<IGuestsListingsRepository, GuestsListingRepository>();
        services.AddScoped<IRoomListingRepository, RoomListingRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
    }
}