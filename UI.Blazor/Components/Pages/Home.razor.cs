using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace UI.Blazor.Components.Pages;
public partial class Home
{
    [Inject] public ILogger<Home> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    //[Inject] public PersistentComponentState ApplicationState { get; set; } = null!;
    //private PersistingComponentStateSubscription _persistingComponentStateSubscription;

    [PersistentState] // 4.Lösung https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management/prerendered-state-persistence?view=aspnetcore-10.0
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation($"{nameof(OnInitializedAsync)} aufgerufen...");

        //3.Lösung
        //_persistingComponentStateSubscription = ApplicationState.RegisterOnPersisting(PersistData);

        //if (!ApplicationState.TryTakeFromJson<QuoteOfTheDayViewModel>(nameof(QotdViewModel), out var restoredData))
        //{
        //    QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
        //}
        //else
        //{
        //    QotdViewModel = restoredData;
        //}

        QotdViewModel ??= await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
    }

    //private Task PersistData()
    //{
    //    ApplicationState.PersistAsJson(nameof(QotdViewModel), QotdViewModel);
    //    return Task.CompletedTask;
    //}

    // 2. Lösung
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
            //StateHasChanged();
            //await JsRuntime.InvokeVoidAsync("myAlert", "Hallo ich bins");
        }
    }
}
