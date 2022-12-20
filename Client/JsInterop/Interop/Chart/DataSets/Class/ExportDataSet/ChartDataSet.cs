using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;
using Client.JsInterop.Interop.Chart.Helper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.ExportDataSet
{
    public class ChartDataSet<T> where T : ChartDataOptionWrapper
    {
        public ChartDataSet()
        {
            _data = new List<T>();
        }

        public void SetDefault()
        {
            _label = ChartHelper.GetChartName(typeof(T));
            _backgroundColor = ChartHelper.GetBackgrouncColor();
            _borderColor = ChartHelper.GetBorderColor();
            _borderWidth = 1;
            _hoverOffset = 4;
            _fill = false;
            _tension = 0.1f;
        }

        public void SetLabel(string label)
        {
            _label = label;
        }

        public void AddData(T data)
        {
            _data.Add(data);
        }

        public void SetBorderWidth(int v)
        {
            _borderWidth = v;
        }

        private string? _label;
        private List<T> _data;
        private List<string>? _backgroundColor;
        private List<string>? _borderColor;
        private int? _borderWidth;
        private int? _hoverOffset;
        private bool? _fill;
        private float? _tension;
        private string? _pointBackgroundColor;
        private string? _pointBorderColor;
        private string? _pointHoverBackgroundColor;
        private string? _pointHoverBorderColor;

    }
}