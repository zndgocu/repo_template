using Microsoft.AspNetCore.Components;

namespace blazor_wasm.Client.Shared
{
    public partial class AppBar
    {
        private bool _toggle { get; set; }

        [Parameter]
        public EventCallback<bool> ToggleChanged { get; set; }


        protected override Task OnInitializedAsync()
        {
            _toggle = false;
            return base.OnInitializedAsync();
        }
        public void DrawerToggle()
        {
            _toggle = !_toggle;
            ToggleChanged.InvokeAsync(_toggle);
        }


    }
}
