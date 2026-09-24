using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Components.Pages;
public partial class Home
{
    [Inject] public ILogger<Home> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation($"{nameof(OnInitializedAsync)} aufgerufen...");

        QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
    }
}
