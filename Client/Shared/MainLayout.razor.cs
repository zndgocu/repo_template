using blazor_wasm.Client.JsInterop.Interop.Chart;
using blazor_wasm.Client.Service;
using EntityContext.Fms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace blazor_wasm.Client.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public JsProviderService? JsProviderService { get; set; }

        [Inject]
        public MatIconProviderService? MatIconProviderService { get; set; }
        
        [Inject]
        public HttpService? HttpService { get; set; }

        [Inject]
        public ISnackbar? Snackbar { get; set; }

        private AppBar? _appBar;
        private NavMenu? _navMenu;
        private bool _toggle { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true)
            {
                if (JsProviderService is not null) await JsProviderService.ShowLoading();
                await WorkFirstRender();
                if (JsProviderService is not null) await JsProviderService.HideLoading();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task WorkFirstRender()
        {
            await this.JsProviderService.JSRepo.AddJsInterop("test", ChartJs.__JS);
            await Task.Delay(0);
        }

        private void OnToggleChanged(bool toggle)
        {
            _toggle = toggle;
        }
    }
}
