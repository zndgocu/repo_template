using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataStruct
{
    public class ChartDataScatterDataType
    {
        private int? _x;
        private int? _y;

        public int? X { get => _x; set => _x = value; }
        public int? Y { get => _y; set => _y = value; }
    }
}