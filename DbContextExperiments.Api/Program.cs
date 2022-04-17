// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System.Threading.Tasks;
using DbContextExperiments.Api.Brokers.DateTimes;
using DbContextExperiments.Api.Brokers.DbContexts.Factory;
using DbContextExperiments.Api.Brokers.DbUpdateExceptions;
using DbContextExperiments.Api.Brokers.Loggings;
using DbContextExperiments.Api.Brokers.Storages;
using DbContextExperiments.Api.Models.Data;
using DbContextExperiments.Api.Models.Foundations.Messages;
using DbContextExperiments.Api.Services.Foundations.Messages;
using DbContextExperiments.Api.Services.Orchestrations.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace DbContextExperiments.Api;

public static class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");

        builder.Services.AddControllers().AddOData(options => options
             .Select().Filter().OrderBy().SetMaxTop(maxTopValue: 100).SkipToken().Count()
             .AddRouteComponents(routePrefix: "odata", OdataConfig.GetEdmModel()));



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

        var services = builder.Services;

        services.AddScoped<IDbUpdateExceptionBroker, DbUpdateExceptionBroker>();
        services.AddScoped<ILoggingBroker, LoggingBroker>();
        services.AddScoped<IDateTimeBroker, DateTimeBroker>();
        services.AddScoped<IStorageContextFactory, StorageContextFactory<ApplicationDbContext>>();
        services.AddScoped<IStorageBroker, StorageBroker>();
        services.AddScoped<IMessageFoundationService, MessageFoundationService>();
        services.AddScoped<IMessageOrchestrationService, MessageOrchestrationService>();

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
public static class OdataConfig
{
    public static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<Message>(name: "Messages");
        return odataBuilder.GetEdmModel();
    }
}