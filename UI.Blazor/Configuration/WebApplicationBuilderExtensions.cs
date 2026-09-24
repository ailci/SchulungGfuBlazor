using Application.Contracts.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI.Blazor.Components.Account;
using UI.Blazor.Data;
using UI.Blazor.Services;

namespace UI.Blazor.Configuration;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        // Blazor Configuration
        public WebApplicationBuilder AddBlazorConfig()
        {
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents(options =>
                {
                    //Detaillierte Fehlermeldung bei Circuit
                    options.DetailedErrors = builder.Environment.IsDevelopment();
                });

            //QotdService DI
            builder.Services.AddScoped<IQotdService, QotdService>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            return builder;
        }

        // Authentication Configuration
        public WebApplicationBuilder AddAuthenticationConfig()
        {
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
                .AddRoles<IdentityRole>() // Add Roles to Identity
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            return builder;
        }
    }
}