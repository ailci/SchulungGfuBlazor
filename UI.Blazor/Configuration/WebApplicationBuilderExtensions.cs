using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI.Blazor.Components.Account;
using UI.Blazor.Data;

namespace UI.Blazor.Configuration;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddBlazorConfig()
        {
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents(options =>
                {
                    //Detaillierte Fehlermeldung
                    options.DetailedErrors = builder.Environment.IsDevelopment();

                }); //Blazor Server

            return builder;
        }

        public WebApplicationBuilder AddAuthentificationConfig()
        {
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            return builder;
        }

        public WebApplicationBuilder AddHttpClients()
        {
            //Named Client
            builder.Services.AddHttpClient("qotdapi", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7083");
                options.DefaultRequestHeaders.Add("Accept","application/json");
            });

            return builder;
        }
    }
}
