using blazor_wasm.Client.JsInterop.Base;
using Microsoft.JSInterop;

namespace blazor_wasm.Client.JsInterop.Interop.Index
{
    public class IndexJs : JsInteropWrapper
    {
        public const string __JS ="~/js_interop/index_js/index.js";
        public override string? JsPath => __JS;
        public IndexJs(string? id, IJSObjectReference? jsObject) : base(id, jsObject)
        {
            
        }
    }
}
