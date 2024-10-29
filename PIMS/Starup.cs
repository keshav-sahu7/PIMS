using Microsoft.EntityFrameworkCore;
using PIMS.Utility;

public class Startup
{
    private IConfiguration _configuration { get; }

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Configure services
    public void ConfigureServices(IServiceCollection services)
    {
        // Add controllers
        services.AddControllers();

        services.AddPimsContext(_configuration);
        // Add Swagger for API documentation
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // Configure the HTTP request pipeline
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