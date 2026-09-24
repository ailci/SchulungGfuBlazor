using Application.ViewModels.Author;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Components.Pages.Author;
public partial class Overview
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public QotdDbContext QotdDbContext { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public IEnumerable<AuthorViewModel>? AuthorsVm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("Author Overview aufgerufen...");
        await GetAuthorsAsync();
    }

    public async Task GetAuthorsAsync()
    {
        var authors = await QotdDbContext.Authors.OrderBy(c => c.Name).ToListAsync();
        
        AuthorsVm = authors.Select(a => new AuthorViewModel
        {
            Id = a.Id,
            Name = a.Name,
            BirthDate = a.BirthDate,
            Description = a.Description,
            Photo = a.Photo,
            PhotoMimeType = a.PhotoMimeType
        });
    }

    public void NavigateToAuthorNew()
    {
        NavManager.NavigateTo("/authors/new");
    }
}
