using Application.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace UI.Blazor.Components.Pages.Author;
public partial class Overview
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
}
