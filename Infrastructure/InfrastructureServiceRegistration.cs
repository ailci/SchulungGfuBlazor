using System;
using System.Collections.Generic;
using System.Text;
using Application.Contracts.Services;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        //DB
        services.AddDbContext<QotdDbContext>(options =>
        {
            options.UseSqlServer(connectionString); //SqlServer
        });

        //DI
        services.AddScoped<IQotdService, QotdService>();
        services.AddScoped<IServiceManager, ServiceManager>();

        return services;
    }
}