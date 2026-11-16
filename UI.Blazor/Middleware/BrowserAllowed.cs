using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UI.Blazor.Middleware;

public enum Browser
{
    InternetExplorer,
    Firefox,
    Chrome,
    Edge,
    Opera,
    SomethingElse
}

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class BrowserAllowed
{
    private readonly RequestDelegate _next;
    private readonly IEnumerable<Browser> _browserWhiteList;

    public BrowserAllowed(RequestDelegate next, IEnumerable<Browser> browserWhiteList)
    {
        _next = next;
        _browserWhiteList = browserWhiteList;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var clientBrowserType = IdentifyBrowser(httpContext);

        if (_browserWhiteList.Any(browser => browser == clientBrowserType)) //Erfolgsfall Browser in White list vorhanden
        {
            await _next(httpContext);
        }
        else //Nicht vorhanden
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            httpContext.Response.ContentType = "text/html";
            await httpContext.Response.WriteAsync(
                $"Browser: {clientBrowserType} nicht erlaubt. Du kommst hier nicht rein!");
        }

    }

    private Browser IdentifyBrowser(HttpContext httpContext)
    {
        var userAgent = httpContext.Request.Headers["User-Agent"][0]?.ToLower();
        Browser browser;

        if (userAgent.Contains("chrome") &&
            !(userAgent.Contains("edge") || userAgent.Contains("edg") || userAgent.Contains("opr")))
        {
            browser = Browser.Chrome;
        }
        else if (userAgent.Contains("firefox"))
        {
            browser = Browser.Firefox;
        }
        else if (userAgent.Contains("trident"))
        {
            browser = Browser.InternetExplorer;
        }
        else if (userAgent.Contains("edge") || userAgent.Contains("edg"))
        {
            browser = Browser.Edge;
        }
        else if (userAgent.Contains("opr"))
        {
            browser = Browser.Opera;
        }
        else
        {
            browser = Browser.SomethingElse;
        }

        return browser;
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class BrowserAllowedExtensions
{
    public static IApplicationBuilder UseBrowserAllowed(this IApplicationBuilder builder, params IEnumerable<Browser> browserWhiteList)
    {
        return builder.UseMiddleware<BrowserAllowed>(browserWhiteList);
    }
}