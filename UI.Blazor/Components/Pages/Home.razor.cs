using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Components.Pages;

public partial class Home //: IDisposable
{
    [PersistentState]
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public PersistentComponentState ApplicationState { get; set; } = null!;
    private PersistingComponentStateSubscription _persistingComponentStateSubscription;

    protected override async Task OnInitializedAsync()
    {
        //3.Lösung
        //_persistingComponentStateSubscription = ApplicationState.RegisterOnPersisting(PersistData);
        //if (!ApplicationState.TryTakeFromJson<QuoteOfTheDayViewModel>(nameof(QotdViewModel), out var restoredData))
        //{
        //    QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
        //}
        //else //gefunden
        //{
        //    QotdViewModel = restoredData;
        //}

        //4.Lösung
        QotdViewModel ??= await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson(nameof(QotdViewModel), QotdViewModel);
        return Task.CompletedTask;
    }

    // 2. Lösung
    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
    //        StateHasChanged();
    //    }
    //}
    //public void Dispose() => _persistingComponentStateSubscription.Dispose();
}
