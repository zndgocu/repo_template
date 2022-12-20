using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.ExportDataSet;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.Wrapper;
using blazor_wasm.Client.JsInterop.Interop.Chart;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.DataWrap;
using blazor_wasm.Client.JsInterop.Interop.Chart.DataSets.Class.OptionWrap;

namespace Client.JsInterop.Interop.Chart.Helper
{
    public static class ChartHelper
    {
        public static readonly Dictionary<Type, ChartType> ChartDataMap = new Dictionary<Type, ChartType>()
        {
            { typeof(ChartDataBar), ChartType.Bar }
            , { typeof(ChartDataBubble), ChartType.Bubble }
            , { typeof(ChartDataDoughnut), ChartType.Doughnut }
            , { typeof(ChartDataLine), ChartType.Line }
            , { typeof(ChartDataPie), ChartType.Pie }
            , { typeof(ChartDataPolar), ChartType.Polar }
            , { typeof(ChartDataRadar), ChartType.Radar }
            , { typeof(ChartDataScatter), ChartType.Scatter }
        };

        public static string GetChartName(Type t)
        {
            ChartType chartType;
            if(ChartDataMap.TryGetValue(t, out chartType) == false)
            {
                return t.Name;
            }
            return ChartConst.GetChartTypeString(chartType);
        }

        public static readonly Dictionary<Type, ChartType> ChartOptionMap = new Dictionary<Type, ChartType>()
        {
            { typeof(ChartOptionBar), ChartType.Bar }
            , { typeof(ChartOptionBubble), ChartType.Bubble }
            , { typeof(ChartOptionDoughnut), ChartType.Doughnut }
            , { typeof(ChartOptionLine), ChartType.Line }
            , { typeof(ChartOptionPie), ChartType.Pie }
            , { typeof(ChartOptionPolar), ChartType.Polar }
            , { typeof(ChartOptionRadar), ChartType.Radar }
            , { typeof(ChartOptionScatter), ChartType.Scatter }
        };

         
        public static List<string> GetBackgrouncColor(int needCount = 20)
        {
            return ChartConst.BackgroundColor.Take(needCount).ToList();
        }
        public static List<string> GetBorderColor(int needCount = 20)
        {
            return ChartConst.BackgroundColor.Take(needCount).ToList();
        }


        public static ChartConfig<T>? CreateChart<T, U>(ChartSetup<T> setup, ChartOption option)
                                                            where T : ChartDataWrapper
                                                            where U : ChartOptionWrapper
        {
            try
            {
                ChartConfig<T> chartConfig = new ChartConfig<T>();
                chartConfig.SetSetup(setup);
                chartConfig.SetOption(option);
                return chartConfig;
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }



        public static ChartConfig<ChartDataBar> GetChartBar()
        {
            var chartSetup = CreateChartSetup<ChartDataBar>();
            chartSetup.SetLabels(new List<string>() { "A", "B" });

            var dataSet = chartSetup.AddDataSets(CreateChartDataSet<ChartDataBar>());
            dataSet.SetDefault();
            dataSet.SetLabel("Stack Label 1");

            var data = new ChartDataBar();
            data.SetData(new List<object>(){1, 2, 3});
            dataSet.AddData(data);
            dataSet.SetBorderWidth(1);

            var data2 = new ChartDataBar();
            data.SetData(new List<object>(){1, 2, 3});
            dataSet.AddData(data);
            dataSet.SetBorderWidth(1);

            var chartOption = new ChartOption();
            ChartOptionBar chartOptionBar = new();
            chartOptionBar.SetAxis(ChartOptionAxis.X);
            chartOption.SetOption(new ChartOptionBar());

            var chartConfig = new ChartConfig<ChartDataBar>();
            chartConfig.SetSetup(chartSetup);
            chartConfig.SetOption(chartOption);

            return chartConfig;
        }


        public static ChartSetup<T> CreateChartSetup<T>() where T : ChartDataWrapper
        {
            return new ChartSetup<T>();
        }

        public static ChartDataSet<T> CreateChartDataSet<T>() where T : ChartDataWrapper
        {
            return new ChartDataSet<T>();
        }
    }
}