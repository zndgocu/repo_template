using Microsoft.AspNetCore.Components;
using blazor_wasm.Client.Service;
using MudBlazor;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems;

namespace blazor_wasm.Client.Pages.TemplatePages
{
    public partial class FlexTemplate
    {
        private string? ParmBackupTemplateId { get; set; }

        [Parameter]
        public string? TemplateId { get; set; }

        [Inject]
        public HttpService? HttpService { get; set; }

        [Inject]
        public ISnackbar? SnackBar { get; set; }

        private TemplateItem? TemplateItem { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }
    }
}