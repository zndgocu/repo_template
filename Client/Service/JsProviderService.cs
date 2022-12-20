using blazor_wasm.Client.JsInterop.Container;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor_wasm.Client.Service
{
    public class JsProviderService
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public JsInteropRepository JSRepo { get; set; }

        public JsProviderService(IJSRuntime jSRuntime, JsInteropRepository jSRepo)
        {
            JSRuntime = jSRuntime;
            JSRepo = jSRepo;
        }

        public async void Alert(string message)
        {
            await JSRepo.Alert(message);
        }

        public async Task ShowLoading()
        {
            await JSRepo.ShowLoading();
        }
        public async Task HideLoading()
        {
            await JSRepo.HideLoading();
        }

    }
}