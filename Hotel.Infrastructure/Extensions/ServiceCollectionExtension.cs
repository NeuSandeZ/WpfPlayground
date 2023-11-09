using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, string connectionString )
    {
        serviceCollection.AddDbContext<HotelDbContext>(builder =>
        {
            builder.UseSqlServer(connectionString);
        });
    }
}