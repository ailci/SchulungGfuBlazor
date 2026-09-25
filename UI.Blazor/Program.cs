using Application;
using Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI.Blazor.Components;
using UI.Blazor.Components.Account;
using UI.Blazor.ComponentsLibrary;
using UI.Blazor.Configuration;
using UI.Blazor.Data;
using UI.Blazor.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddBlazorConfig()
    .AddAuthenticationConfig()
    .AddSerilogConfig()
    .AddHttpConfig();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .ConfigComponentLibrary(); 

var app = builder.Build();

// Configure the HTTP request pipeline. ##############################################################################################
//app.Use(async (context, next) =>
//{
//    var userAgent = context.Request.Headers["User-Agent"][0]?.ToLower();
//    await context.Response.WriteAsync($"First middleware\n {userAgent}");
//    await next();
//    await context.Response.WriteAsync("First middleware Rückweg\n");
//});
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Second middleware\n");
//    await next();
//});

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("End middleware\n");
//});

//app.UseBrowserAllowed(Browser.Chrome, Browser.Edge);
//----------------------------------------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(UI.Blazor.ComponentsLibrary._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
