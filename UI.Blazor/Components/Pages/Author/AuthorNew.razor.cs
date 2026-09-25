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

    private Task HandleValidSubmit(EditContext arg)
    {
        //TODO: Implementierung
        Logger.LogInformation($"CreateForAuthorVm => {AuthorForCreateVm?.LogAsJson()}");

        return Task.CompletedTask;
    }

    private void OnInputFileChange(InputFileChangeEventArgs args)
    {
        AuthorForCreateVm!.Photo = args.File;
        //Logger.LogInformation($"Photo => {args.LogAsJson()}");
    }
}