using Application.Contracts.Services;
using Application.Utilities;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace UI.Blazor.Components.Pages.Author;
public partial class AuthorNew
{
    [Inject] public ILogger<AuthorNew> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }
    public IBrowserFile? SelectedAuthorPhoto { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new() { Name = "", Description = "" };
    private async Task HandleValidSubmit(EditContext args)
    {
        try
        {
            Logger.LogInformation($"AuthorForCreateVm: {AuthorForCreateVm?.LogAsJson()}");
            var newAuthorVm = await ServiceManager.AuthorService.AddAuthorAsync(AuthorForCreateVm!);

            if (newAuthorVm is not null)
            {
                NavManager.NavigateTo("/authors/overview");
            }
        }
        catch (Exception e)
        {
            Logger.LogError($"Author konnte nicht gespeichert werden: {e.Message}");
        }
    }
    private void OnInputFileChange(InputFileChangeEventArgs args)
    {
        //Logger.LogInformation($"Photo: {args.File.LogAsJson()}");
        AuthorForCreateVm!.Photo = SelectedAuthorPhoto = args.File;
    }
}
