using Application.Utilities;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UI.Blazor.Components.Pages.Author;
using UI.Blazor.ComponentsLibrary;
using UI.Blazor.ComponentsLibrary.Components;

namespace UI.Blazor.Components;
public partial class AuthorTable
{
    [Inject] public ILogger<AuthorTable> Logger { get; set; } = null!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] public DialogService DialogService { get; set; } = null!;
    [Parameter, EditorRequired] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }
    [Parameter] public EventCallback<Guid> OnAuthorDelete { get; set; }
    private Guid _authorIdToDelete;
    private ConfirmDialog? _confirmDialogComponent;

    private async Task ShowConfirmation(AuthorViewModel authorVm)
    {
        Logger.LogInformation($"AuthorVm: {authorVm.LogAsJson()}");

        _authorIdToDelete = authorVm.Id;

        //1. Version
        //if (await JsRuntime.InvokeAsync<bool>("myConfirm", $"Wollen Sie wirklich den Autor '{authorVm?.Name}' löschen?"))
        //{
        //    await OnAuthorDelete.InvokeAsync(authorVm!.Id); //Ereignis auslösen
        //}

        //2. Version via Service
        //if (await DialogService.ConfirmAsync($"Wollen Sie wirklich den Autor '{authorVm?.Name}' löschen?"))
        //{
        //    await OnAuthorDelete.InvokeAsync(authorVm!.Id); //Ereignis auslösen
        //}

        //3. Version als Componente
        _confirmDialogComponent?.Show($"Wollen Sie wirklich den Autor '{authorVm?.Name}' löschen?");
    }
    private async Task ConfirmDeleteClicked(bool deleteClicked)
    {
        if (deleteClicked)
        {
            await OnAuthorDelete.InvokeAsync(_authorIdToDelete);
        }
    }
}
