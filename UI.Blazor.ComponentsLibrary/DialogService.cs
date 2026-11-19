using Microsoft.JSInterop;

namespace UI.Blazor.ComponentsLibrary
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class DialogService(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask = new(async () =>
        {
            try
            {
                return await jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/UI.Blazor.ComponentsLibrary/dialogmodule.js").AsTask();
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException("Unable to import the JavaScript module.", ex);
            }
        });

        public async ValueTask<bool> ConfirmAsync(string message)
        {
            var module = await _moduleTask.Value;

            try
            {
                return await module.InvokeAsync<bool>("showConfirm", message);
            }
            catch (JSException ex)
            {
                throw new InvalidOperationException("Unable to invoke the showConfirm JavaScript function.", ex);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
