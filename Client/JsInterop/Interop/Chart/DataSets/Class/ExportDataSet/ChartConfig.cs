using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.ExportDataSet
{
    public class ChartConfig<T> where T : ChartDataWrapper
    {
        public ChartConfig()
        {
            _data = new ChartSetup<T>();
            _options = new ChartOption();
        }

        private string? _type;
        private ChartSetup<T> _data;
        private ChartOption _options;

        public void SetSetup(ChartSetup<T> setup)
        {
            _data = setup;
        }

        public void SetOption(ChartOption option)
        {
            _options = option;
        }
    }
}