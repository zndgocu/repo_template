using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataStruct;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataWrap
{
    public class ChartDataScatter : ChartDataWrapper
    {
        public ChartDataScatter()
        {
        }

        protected override ChartType ChartType => ChartType.Scatter;
        private List<ChartDataScatterDataType>? _data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas">List<ChartDataScatterDataType></param>
        public override void SetData(List<object> datas)
        {
            _data = new();
            foreach (var data in datas)
            {
                try
                {
                    var scatterData = (ChartDataScatterDataType)data;
                    _data.Add(scatterData);
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
        }

    }
}