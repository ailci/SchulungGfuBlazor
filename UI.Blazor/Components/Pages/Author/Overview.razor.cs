using Application.Contracts.Services;
using Application.ViewModels.Author;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Components.Pages.Author;
public partial class Overview
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public IEnumerable<AuthorViewModel>? AuthorsVm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("Author Overview aufgerufen...");
        await GetAuthorsAsync();
    }

    public async Task GetAuthorsAsync()
    {
        AuthorsVm = (await ServiceManager.AuthorService.GetAuthorsAsync()).OrderBy(c => c.Name);
    }

    public void NavigateToAuthorNew()
    {
        NavManager.NavigateTo("/authors/new");
    }
}
