using Microsoft.EntityFrameworkCore;
using PIMS.Repository;
using PIMS.Services.CategoryServices;
using PIMS.Services.InventoryServices;
using PIMS.Services.ProductServices;
using PIMS.Services.UserServices;

public class Startup
{
    private IConfiguration _configuration { get; }

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.RegisterRepository(_configuration["DbConnectionString"]);
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IInventoryService, InventoryService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();
    }
}