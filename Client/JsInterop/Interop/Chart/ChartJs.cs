using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Base;
using Microsoft.JSInterop;

namespace blazor_wasm.Client.JsInterop.Interop.Chart
{
    public class ChartJs : JsInteropWrapper
    {
        public const string __JS = "~/js_interop/chart_js/chart.js";
        public override string? JsPath => __JS;
        public ChartJs(string? id, IJSObjectReference? jsObject) : base(id, jsObject)
        {
            
        }
    }
}