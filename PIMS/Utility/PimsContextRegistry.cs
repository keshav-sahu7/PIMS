using PIMS.Models;

namespace PIMS.Utility;

using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;



public static class PimsContextRegistry
{
    public static IServiceCollection AddPimsContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = "Server=localhost;Port=3306;Database=InventoryDB;User=user1;Password=Pass2024;"; //TODO: remove
        services.AddDbContext<PimsContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        return services;
    }
}

