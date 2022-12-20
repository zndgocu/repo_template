using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems.Inherits
{
    public class TemplateItemRenderTree : ComponentBase
    {
        protected override async Task OnParametersSetAsync()
        {
            await SetRenderData();
            await base.OnParametersSetAsync();
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual Task SetRenderData()
        {
            return Task.FromResult(1);
        }
    }
}