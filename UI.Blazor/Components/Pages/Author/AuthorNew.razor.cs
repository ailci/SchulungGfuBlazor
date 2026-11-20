using Application.Utilities;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace UI.Blazor.Components.Pages.Author;
public partial class AuthorNew
{
    [Inject] public ILogger<AuthorNew> Logger { get; set; } = null!;
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }
    public IBrowserFile? SelectedAuthorPhoto { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new() { Name = "", Description = "" };
    private Task HandleValidSubmit(EditContext args)
    {
        try
        {
            Logger.LogInformation($"AuthorForCreateVm: {AuthorForCreateVm?.LogAsJson()}");
        }
        catch (Exception e)
        {
            
        }

        return Task.CompletedTask;
    }
    private void OnInputFileChange(InputFileChangeEventArgs args)
    {
        //Logger.LogInformation($"Photo: {args.File.LogAsJson()}");
        AuthorForCreateVm!.Photo = SelectedAuthorPhoto = args.File;
    }
}
