using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataStruct
{
    public class ChartDataBubbleDataType
    {
        private int? _x;
        private int? _y;
        private int? _r;

        public int? X { get => _x; set => _x = value; }
        public int? Y { get => _y; set => _y = value; }
        public int? R { get => _r; set => _r = value; }
    }
}