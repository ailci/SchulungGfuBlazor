using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Blazor.ComponentsLibrary;

public class DialogService(IJSRuntime jsRuntime) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "./_content/UI.Blazor.ComponentsLibrary/js/dialog.js").AsTask());

    public async ValueTask<bool> ConfirmAsync(string message)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("myConfirm", message);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}