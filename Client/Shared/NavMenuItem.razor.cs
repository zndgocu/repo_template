using EntityContext.Fms;
using Microsoft.AspNetCore.Components;

namespace blazor_wasm.Client.Shared
{
    public partial class NavMenuItem
    {
        [Parameter]
        public MenuItem? MenuItem { get; set; }

        [CascadingParameter]
        public NavigationManager? NavigationManager { get; set; }

        public void NavTo(string? url, bool refresh = false)
        {
            if (NavigationManager is not null)
            {
                if (url is not null)
                {
                    NavigationManager.NavigateTo(url, refresh);
                }
            }
        }
    }
}