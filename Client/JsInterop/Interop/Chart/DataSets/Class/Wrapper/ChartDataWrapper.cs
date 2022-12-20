using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper
{
    public abstract class ChartDataWrapper : ChartDataOptionWrapper
    {
        public ChartDataWrapper()
        {

        }
        

        protected override ChartType BaseChartType => ChartType;
        protected abstract ChartType ChartType { get; }

        public abstract void SetData(List<object> datas);
    }
}