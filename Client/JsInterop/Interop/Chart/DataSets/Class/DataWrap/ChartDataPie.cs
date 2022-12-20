using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataWrap
{
    public class ChartDataPie : ChartDataWrapper
    { 
        public ChartDataPie()
        {
        }
        
        protected override ChartType ChartType => ChartType.Pie;

        private List<int>? _data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas">List<int></param>
        public override void SetData(List<object> datas)
        {
            _data = new();
            foreach (var data in datas)
            {
                try
                {
                    _data.Add((int)data);
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
        }

    }
}