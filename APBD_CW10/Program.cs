using APBD_CW10.Data;
using APBD_CW10.Repositories;
using APBD_CW10.Services;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW10;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        // &(SCOPED)&
        builder.Services.AddOpenApi();
        
        builder.Services.AddScoped<ITripsService, TripsService>();
        builder.Services.AddScoped<ITripsRepository, TripsRepository>();
        builder.Services.AddScoped<IClientsService, ClientsService>();
        builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
        
        builder.Services.AddDbContext<ApbdCw10Context>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default-db"));
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}