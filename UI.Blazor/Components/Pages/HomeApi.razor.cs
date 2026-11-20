using System.Text.Json;
using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using UI.Blazor.Components.Pages.Author;

namespace UI.Blazor.Components.Pages;

public partial class HomeApi
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = null!;
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }
   
  
    protected override async Task OnInitializedAsync()
    {
        try
        {
            //1.Version Klassik
            var client = HttpClientFactory.CreateClient("qotdapi");
            var response = await client.GetAsync("api/qotd");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            QotdViewModel = JsonSerializer.Deserialize<QuoteOfTheDayViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});


        }
        catch (HttpRequestException e)
        {
            Logger.LogError(e.Message);
        }
    }
}
