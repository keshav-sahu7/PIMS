using Microsoft.EntityFrameworkCore;
using PIMS.Models;

namespace PIMS.Repository;

public static class UnitOfWorkRegistration
{
    public static IServiceCollection RegisterRepository( this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PimsContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}