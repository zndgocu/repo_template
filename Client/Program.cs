using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor_wasm.Client;
using MudBlazor.Services;
using blazor_wasm.Client.JsInterop.Container;
using blazor_wasm.Client.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient("blazor_wasm.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("blazor_wasm.ServerAPI"));
builder.Services.AddSingleton<JsInteropRepository>();
builder.Services.AddSingleton<JsProviderService>();
builder.Services.AddScoped<MatIconProviderService>();
builder.Services.AddTransient<HttpService>();

await builder.Build().RunAsync();
