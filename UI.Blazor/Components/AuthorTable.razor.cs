using Application.Utilities;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using UI.Blazor.Components.Pages.Author;

namespace UI.Blazor.Components;
public partial class AuthorTable
{
    [Inject] public ILogger<AuthorTable> Logger { get; set; } = null!;
    [Parameter, EditorRequired] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }
    [Parameter] public EventCallback<Guid> OnAuthorDelete { get; set; }

    private async Task ShowConfirmation(AuthorViewModel authorVm)
    {
        Logger.LogInformation($"AuthorVm: {authorVm.LogAsJson()}");

        await OnAuthorDelete.InvokeAsync(authorVm.Id);
    }
}
