using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.ExportDataSet
{
    public class ChartOption
    {
        public ChartOption()
        {
            _options = new();
        }

        private string? _type;

        private Dictionary<Type, ChartOptionWrapper> _options;

        public void SetOption<T>(T option) where T : ChartOptionWrapper
        {
            ChartOptionWrapper? v;
            if (_options.TryGetValue(option.GetType(), out v) == false)
            {
                v = option;
            }
            _options.Add(v.GetType(), option);
        }
    }
}