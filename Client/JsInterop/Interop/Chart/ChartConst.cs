using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Extensions.Extension;

namespace blazor_wasm.Client.JsInterop.Interop.Chart
{
    public enum ChartType
    {
        [Description("bar")]
        Bar,
        [Description("bubble")]
        Bubble,
        [Description("doughnut")]
        Doughnut,
        [Description("pie")]
        Pie,
        [Description("line")]
        Line,
        [Description("polarArea")]
        Polar,
        [Description("radar")]
        Radar,
        [Description("scatter")]
        Scatter,
    }

    public enum ChartOptionAnimationEasing
    {

        [Description("linear")]
        Linear,
        [Description("easeInQuad")]
        EaseInQuad,
        [Description("easeOutQuad")]
        EaseOutQuad,
        [Description("easeInOutQuad")]
        EaseInOutQuad,
        [Description("easeInCubic")]
        EaseInCubic,
        [Description("easeOutCubic")]
        EaseOutCubic,
        [Description("easeInOutCubic")]
        EaseInOutCubic,
        [Description("easeInQuart")]
        EaseInQuart,
        [Description("easeOutQuart")]
        EaseOutQuart,
        [Description("easeInOutQuart")]
        EaseInOutQuart,
        [Description("easeInQuint")]
        EaseInQuint,
        [Description("easeOutQuint")]
        EaseOutQuint,
        [Description("easeInOutQuint")]
        EaseInOutQuint,
        [Description("easeInSine")]
        EaseInSine,
        [Description("easeOutSine")]
        EaseOutSine,
        [Description("easeInOutSine")]
        EaseInOutSine,
        [Description("easeInExpo")]
        EaseInExpo,
        [Description("easeOutExpo")]
        EaseOutExpo,
        [Description("easeInOutExpo")]
        EaseInOutExpo,
        [Description("easeInCirc")]
        EaseInCirc,
        [Description("easeOutCirc")]
        EaseOutCirc,
        [Description("easeInOutCirc")]
        EaseInOutCirc,
        [Description("easeInElastic")]
        EaseInElastic,
        [Description("easeOutElastic")]
        EaseOutElastic,
        [Description("easeInOutElasti")]
        EaseInOutElasti,
        [Description("easeInBack")]
        EaseInBack,
        [Description("easeOutBack")]
        EaseOutBack,
        [Description("easeInOutBack")]
        EaseInOutBack,
        [Description("easeInBounce")]
        EaseInBounce,
        [Description("easeOutBounce")]
        EaseOutBounce,
        [Description("easeInOutBounce")]
        EaseInOutBounce,
    }

    public enum ChartOptionAxis
    {
        [Description("x")]
        X,
        [Description("y")]
        Y,
        [Description("z")]
        Z,
        [Description("r")]
        R,
    }

    public enum ChartOptionScatterAxisType
    {
        [Description("linear")]
        Linear,
    }

    public enum ChartOptionScatterPosition
    {
        [Description("bottom")]
        Bottom,
    }

    public static class ChartConst
    {
        public static readonly List<string> BackgroundColor = new List<string>()
        {
            "rgba(255, 99, 132, 0.2)"
            , "rgba(255, 159, 64, 0.2)"
            , "rgba(255, 205, 86, 0.2)"
            , "rgba(75, 192, 192, 0.2)"
            , "rgba(54, 162, 235, 0.2)"
            , "rgba(153, 102, 255, 0.2)"
            , "rgba(201, 203, 207, 0.2)"
            , "rgba(206, 91, 55, 0.2)"
            , "rgba(26, 244, 28, 0.2)"
            , "rgba(134, 53, 9, 0.2)"
            , "rgba(24, 171, 70, 0.2)"
            , "rgba(151, 174, 56, 0.2)"
            , "rgba(47, 59, 189, 0.2)"
            , "rgba(15, 43, 25, 0.2)"
            , "rgba(62, 121, 68, 0.2)"
            , "rgba(24, 145, 60, 0.2)"
            , "rgba(66, 90, 236, 0.2)"
            , "rgba(85, 121, 139, 0.2)"
            , "rgba(226, 55, 244, 0.2)"
            , "rgba(73, 246, 173, 0.2)"
        };


        public static readonly List<string> BorderColor = new List<string>()
        {
            "rgb(255, 99, 132)"
            , "rgb(255, 159, 64)"
            , "rgb(255, 205, 86)"
            , "rgb(75, 192, 192)"
            , "rgb(54, 162, 235)"
            , "rgb(153, 102, 255)"
            , "rgb(201, 203, 207)"
            , "rgb(206, 91, 55)"
            , "rgb(26, 244, 28)"
            , "rgb(134, 53, 9)"
            , "rgb(24, 171, 70)"
            , "rgb(151, 174, 56)"
            , "rgb(47, 59, 189)"
            , "rgb(15, 43, 25)"
            , "rgb(62, 121, 68)"
            , "rgb(24, 145, 60)"
            , "rgb(66, 90, 236,)"
            , "rgb(85, 121, 139)"
            , "rgb(226, 55, 244)"
            , "rgb(73, 246, 173)"
        };



        public static string GetChartTypeString(ChartType e)
        {
            return e.GetDescription();
        }

        public static string GetChartOptionAnimationEasingString(ChartOptionAnimationEasing e)
        {
            return e.GetDescription();
        }
    }
}