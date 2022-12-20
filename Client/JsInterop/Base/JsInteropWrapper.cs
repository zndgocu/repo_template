using Microsoft.JSInterop;
using System.Security.Cryptography;

namespace blazor_wasm.Client.JsInterop.Base
{
    public class JsInteropWrapper : IDisposable
    {
        public virtual string? JsPath { get; }
        
        private string? _id;
        private readonly IJSObjectReference? _jsObject;

        public string? Id { get => _id; }

        public JsInteropWrapper(string? id, IJSObjectReference? jsObject)
        {
            _id = id;
            _jsObject = jsObject;
        }

        public async void InvokeVoidAsync<T>(string funcName, object[] parms)
        {
            if (_jsObject is not null)
            {
                await _jsObject.InvokeVoidAsync(funcName, parms);
            }
        }

        public async ValueTask<T?> InvokeAsync<T>(string funcName, object[] parms)
        {
            if (_jsObject is not null)
            {
                return await _jsObject.InvokeAsync<T>(funcName, parms);
            }
            return default;
        }

        public void Dispose()
        {
            _jsObject?.DisposeAsync();
        }
    }
}
