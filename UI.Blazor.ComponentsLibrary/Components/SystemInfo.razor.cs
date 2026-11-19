using Application.Utilities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace UI.Blazor.ComponentsLibrary.Components;

public partial class SystemInfo
{
    private bool AssignedRenderModeReached => AssignedRenderMode switch
    {
        InteractiveServerRenderMode => RendererInfo is
        {
            Name: "Server", IsInteractive: true
        },
        InteractiveWebAssemblyRenderMode => RendererInfo.Name == "WebAssembly",
        InteractiveAutoRenderMode => RendererInfo.Name == "WebAssembly",
        _ => true
    };

    private IJSObjectReference _jsModule = null!;
    public Dictionary<string, string> BrowserInfo { get; set; } = [];

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var dotNetObjRef = DotNetObjectReference.Create(this); //Componente als Referenz
            _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/UI.Blazor.ComponentsLibrary/Components/SystemInfo.razor.js");
            await _jsModule.InvokeVoidAsync("GetBrowserInfo", dotNetObjRef, nameof(SetBrowserInfo));
        }
    }

    [JSInvokable]
    public void SetBrowserInfo(Dictionary<string, string> info)
    {
        Logger.LogInformation("SetBrowserInfo aufgerufen...");
        BrowserInfo = info;
        Logger.LogInformation($"BrowserInfo: {BrowserInfo.LogAsJson()}");
        StateHasChanged();
    }
}