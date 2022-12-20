using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataStruct;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataWrap
{
    public class ChartDataBubble : ChartDataWrapper
    {
        public ChartDataBubble()
        {
        }
        protected override ChartType ChartType => ChartType.Bubble;

        private List<ChartDataBubbleDataType>? _data;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas">List<ChartDataBubbleDataType></param>
        public override void SetData(List<object> datas)
        {
            _data = new();
            foreach (var data in datas)
            {
                try
                {
                    var bubbleData = (ChartDataBubbleDataType)data;
                    _data.Add(bubbleData);
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
        }

    }
}