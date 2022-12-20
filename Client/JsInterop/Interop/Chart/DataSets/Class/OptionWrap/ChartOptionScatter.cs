using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.OptionStruct;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.OptionWrap
{
    public class ChartOptionScatter : ChartOptionWrapper
    {
        public ChartOptionScatter()
        {
        }

        protected override ChartType ChartType => ChartType.Bar;
        private ChartOptionAxis? _indexAxis;
    }
}