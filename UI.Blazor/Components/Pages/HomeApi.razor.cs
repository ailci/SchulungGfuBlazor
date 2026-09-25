using System.Text.Json;
using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace UI.Blazor.Components.Pages;
public partial class HomeApi
{
    [Inject] public ILogger<HomeApi> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = null!;
    //[Inject(Key = "qotdapi")] public IQotdService QotdApiService { get; set; }
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation($"{nameof(OnInitializedAsync)} aufgerufen...");

        try
        {
            //1. Variante Klassik
            //var client = HttpClientFactory.CreateClient("qotdapiservice");
            //var response = await client.GetAsync("api/qotd");
            //response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();
            //Logger.LogInformation($"RÜCKGABE API: {content}");
            //QotdViewModel = JsonSerializer.Deserialize<QuoteOfTheDayViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            //2. Variante Abkürzung
            //var client = HttpClientFactory.CreateClient("qotdapiservice");
            //QotdViewModel = await client.GetFromJsonAsync<QuoteOfTheDayViewModel>("api/qotd");

            //3. Variante via Service
            //QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync(); 
            
            //3. Variante via Service und API
            QotdViewModel = await ServiceManager.QotdApiService.GetQuoteOfTheDayAsync();

        }
        catch (HttpRequestException ex)
        {
            
        }
    }
}
