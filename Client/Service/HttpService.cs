using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using blazor_wasm.Client.Shared;

namespace blazor_wasm.Client.Service
{
    public class HttpService
    {
        [Inject]
        public HttpClient? Http { get; set; }

        public HttpService(HttpClient? http){
            this.Http = http;
        }

        public async Task<HttpResponseMessage?> PostAsync(string token, string url, StringContent content)
        {
            if (Http is null) return null;
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await Http.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage?> PostAsync<T>(string token, string url, T item)
        {
            if (Http is null) return null;
            StringContent content = new StringContent(JsonSerialize.SerializeDefault(item), Encoding.UTF8, "application/json");
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await Http.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage?> GetAsync(string token, string url)
        {
            try
            {
                if (Http is null) return null;
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Http.GetAsync(url);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }


        public async Task<HttpResponseMessage?> PutAsync(string token, string url)
        {
            try
            {
                if (Http is null) return null;
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Http.PutAsync(url, null);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }

        public async Task<HttpResponseMessage?> PutAsync(string token, string url, StringContent content)
        {
            try
            {
                if (Http is null) return null;
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Http.PutAsync(url, content);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }
        public async Task<HttpResponseMessage?> PutAsync<T>(string token, string url, T item)
        {
            try
            {
                if (Http is null) return null;
                StringContent content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Http.PutAsync(url, content);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }

        public async Task<HttpResponseMessage?> PatchAsync(string token, string url, StringContent content)
        {
            try
            {
                if (Http is null) return null;
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Http.PatchAsync(url, content);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }

        public async Task<HttpResponseMessage?> PatchAsync<T>(string token, string url, T item)
        {
            try
            {
                if (Http is null) return null;
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonSerialize.SerializeDefault(item), Encoding.UTF8, "application/json");
                return await Http.PatchAsync(url, content);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }


        
        public async Task<HttpResponseMessage?> DeleteAsync(string token, string url)
        {
            try
            {
                if (Http is null) return null;
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Http.DeleteAsync(url);
            }
            catch (System.Exception exAll)
            {
                Console.WriteLine(exAll.Message);
                return null;
            }
        }
    }
}