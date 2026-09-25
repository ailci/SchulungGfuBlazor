using Application.Contracts.Services;
using Application.Utilities;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace UI.Blazor.Components.Pages.Author;

public partial class AuthorNew
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new() { Name = "", Description = "" };

    private async Task HandleValidSubmit(EditContext arg)
    {
        Logger.LogInformation($"CreateForAuthorVm => {AuthorForCreateVm?.LogAsJson()}");

        try
        {
            var newAuthorVm = await ServiceManager.AuthorService.AddAuthorAsync(AuthorForCreateVm!);

            if (newAuthorVm is not null)
            {
                NavManager.NavigateTo("/authors/overview");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Fehler beim Speichern des Autors: {ex.Message}");
        }
    }

    private void OnInputFileChange(InputFileChangeEventArgs args)
    {
        AuthorForCreateVm!.Photo = args.File;
        //Logger.LogInformation($"Photo => {args.LogAsJson()}");
    }
}