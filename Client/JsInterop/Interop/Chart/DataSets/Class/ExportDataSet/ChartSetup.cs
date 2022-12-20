using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataWrap;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.ExportDataSet
{
    public class ChartSetup<T> where T : ChartDataWrapper
    {
        public ChartSetup()
        {
            _dataSets = new();
        }

        private List<string>? _labels;
        private List<ChartDataSet<T>> _dataSets;

        public void SetLabels(List<string> labels)
        {
            _labels = labels;
        }

        public ChartDataSet<T> AddDataSets(ChartDataSet<T> chartDataSet) 
        {
            _dataSets.Add(chartDataSet);
            return chartDataSet;
        }
    }
}