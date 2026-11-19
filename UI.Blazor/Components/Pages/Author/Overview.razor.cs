using Application.Contracts.Services;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;

namespace UI.Blazor.Components.Pages.Author;

public partial class Overview
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public IEnumerable<AuthorViewModel>? AuthorsVm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAuthorsAsync();
    }

    private async Task GetAuthorsAsync()
    {
        AuthorsVm = (await ServiceManager.AuthorService.GetAuthorsAsync()).OrderBy(c => c.Name);
    }

    private async Task DeleteAuthor(Guid authorId)
    {
        Logger.LogInformation($"AuthorId zum Löschen lautet: {authorId}");

        try
        {
            var isDeleted = await ServiceManager.AuthorService.DeleteAuthorAsync(authorId);

            if (isDeleted)
            {
                Logger.LogInformation($"Author mit ID: {authorId} erfolgreich gelöscht");
                await GetAuthorsAsync();
            }
            else
            {
                Logger.LogError($"Author mit ID: {authorId} konnte NICHT gelöscht werden");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Author mit ID: {authorId} konnte NICHT gelöscht werden. Message: {ex.Message}");
        }
    }
    private void NavigateToAuthorNew()
    {
        NavManager.NavigateTo("/authors/new");
    }
}
