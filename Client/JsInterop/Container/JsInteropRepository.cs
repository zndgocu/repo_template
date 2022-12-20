using blazor_wasm.Client.JsInterop.Base;
using Microsoft.JSInterop;

namespace blazor_wasm.Client.JsInterop.Container
{
    public class JsInteropRepository : IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private Dictionary<string, JsInteropWrapper>? _jss;

        public JsInteropRepository(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _jss = new Dictionary<string, JsInteropWrapper>();
        }

        public async Task Alert(string message)
        {
            await _jsRuntime.InvokeVoidAsync("alert", message);
        }
        public async Task ShowLoading()
        {
            await _jsRuntime.InvokeVoidAsync("ShowLoading");
        }

        public async Task HideLoading()
        {
            await _jsRuntime.InvokeVoidAsync("HideLoading");
        }

        /// <summary>
        /// import
        /// </summary>
        /// <param name="id">module id</param>
        /// <param name="path">module path</param>
        /// <returns></returns>
        public async ValueTask<bool> AddJsInterop(string id, string path)
        {
            if(_jss is null) return false;
            if (_jss.ContainsKey(id) is true) return true;
            try
            {
                var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", path);
                JsInteropWrapper js = new JsInteropWrapper(id, module);
                return AddJsInterop(js);
            }
            catch (Exception exception)
            {
                await Alert(exception.Message);
                return false;
            }
        }

        public bool AddJsInterop(JsInteropWrapper jsInterop)
        {
            if(_jss is null) throw new Exception("_jss is null");
            if (jsInterop.Id is null) throw new Exception("id not defined");
            if (_jss.ContainsKey(jsInterop.Id)) throw new Exception($"already js id {jsInterop.Id}");
            _jss?.Add(jsInterop.Id, jsInterop);
            return true;
        }

        public void Dispose()
        {
            if(_jss != null)
            {
                foreach(var js in _jss.Values)
                {
                    js.Dispose();
                }
                _jss.Clear();
                _jss = null;
            }
        }

    }
}
