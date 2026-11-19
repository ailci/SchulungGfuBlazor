using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Blazor.ComponentsLibrary;

public static class ComponentsLibraryServiceRegistration
{
    public static IServiceCollection AddComponentsLibraryServices(this IServiceCollection services)
    {
        //Dialog
        services.AddScoped<DialogService>();

        return services;
    }
}