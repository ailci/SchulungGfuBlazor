using Microsoft.Extensions.DependencyInjection;

namespace UI.Blazor.ComponentsLibrary;

public static class ServiceComponentLibRegistration
{
    public static IServiceCollection ConfigComponentLibrary(this IServiceCollection services)
    {
        //DialogService
        services.AddScoped<DialogService>();

        return services;
    }
}