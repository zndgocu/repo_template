using Microsoft.AspNetCore.Components;
using blazor_wasm.Client.Service;
using Shared.ApiResult;
using MudBlazor;
using blazor_wasm.Client.Shared;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems;
using EntityContext.Fms;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems.Inherits;
using Extensions.Extension;
using Client.JsInterop.Interop.Chart.Helper;
using blazor_wasm.Client.JsInterop.Interop.Chart;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems
{
    public partial class TemplateItemChart
    {
        [Inject]
        public JsProviderService? JsProviderService { get; set; }

        [CascadingParameter]
        public TemplateItemDTO? TemplateItemDTO { get; set; }

        [Parameter]
        public HttpService? HttpService { get; set; }

        [Parameter]
        public ISnackbar? SnackBar { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            JsProviderService.JSRepo.AddJsInterop("asdf", ChartJs.__JS);
            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task Test()
        {
            var vv = ChartHelper.GetChartBar();
            string aa = JsonSerialize.SerializeDefault(vv);
            if (JsProviderService is not null)
            {
                await JsProviderService.JSRuntime.InvokeAsync<int>("DrawChart", new object[] { vv });
            }
        }
    }
}