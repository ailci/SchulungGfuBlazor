using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Components.Pages;

public partial class Home
{
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    [Inject] public IQotdService QotdService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        QotdViewModel = await QotdService.GetQuoteOfTheDayAsync();
    }
}
