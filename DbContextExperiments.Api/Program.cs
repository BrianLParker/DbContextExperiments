using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DbContextExperiments.Api.Data;

namespace DbContextExperiments.Api;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");

        builder.Services.AddDbContextFactory<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString);
        })
       .AddScoped(serviceProvider =>
            serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
           .CreateDbContext());

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}
