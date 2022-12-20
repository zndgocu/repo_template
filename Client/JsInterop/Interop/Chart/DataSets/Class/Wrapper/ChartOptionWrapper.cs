using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper
{
    public abstract class ChartOptionWrapper : ChartDataOptionWrapper
    {
        public ChartOptionWrapper()
        {

        }

        protected override ChartType BaseChartType => ChartType;
        protected abstract ChartType ChartType { get; }

        public virtual void SetAxis(ChartOptionAxis axis)
        {

        }

    }
}