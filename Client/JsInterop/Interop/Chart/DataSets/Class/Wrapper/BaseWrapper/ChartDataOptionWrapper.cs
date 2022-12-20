using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper
{
    public abstract class ChartDataOptionWrapper : IChartDataOptionWrapper
    {
        public ChartDataOptionWrapper()
        {

        }

        protected abstract ChartType BaseChartType { get; }
    }
}