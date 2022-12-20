using Extensions.Extension;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace blazor_wasm.Client.Service
{
    public class MatIconProviderService
    {
        public readonly string[] __DEFAULT_ICONS =
        {
            "svg_icon/action/123/materialicons/24px.svg",
        };
        public const string __DEFAULT_ICON = "<g><rect fill=\"none\" height=\"24\" width=\"24\"/></g><g><path d=\"M7,15H5.5v-4.5H4V9h3V15z M13.5,13.5h-3v-1h2c0.55,0,1-0.45,1-1V10c0-0.55-0.45-1-1-1H9v1.5h3v1h-2c-0.55,0-1,0.45-1,1V15 h4.5V13.5z M19.5,14v-4c0-0.55-0.45-1-1-1H15v1.5h3v1h-2v1h2v1h-3V15h3.5C19.05,15,19.5,14.55,19.5,14z\"/></g>";

        [Inject]
        public HttpClient HttpClient { get; set; }

        public MatIconProviderService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<string> GetSvgString(string? path)
        {
            string message = "";
            var img = await HttpClient.GetAsync(path);
            if (img.IsOk(out message) == false)
            {
                return __DEFAULT_ICON;
            }
            return await img.Content.GetContentString();
        }

        public async Task<List<string>> GetDefaultIconsString()
        {
            List<string> icons = new List<string>();
            foreach(var icon in __DEFAULT_ICONS)
            {
                icons.Add(await GetSvgString(icon));
            }
            return icons;
        }
    }
}