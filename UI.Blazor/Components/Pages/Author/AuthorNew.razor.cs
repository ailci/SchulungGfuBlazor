using Application.ViewModels.Author;

namespace UI.Blazor.Components.Pages.Author;
public partial class AuthorNew
{
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new() { Name = "", Description = "" };
}
