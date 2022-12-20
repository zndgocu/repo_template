using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems;
using EntityHelper;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems
{
    public class TemplateItemDTO
    {
        public static readonly string TemplateItemTable = "table";
        public static readonly string TemplateItemChart = "chart";
        public TemplateItemDTO()
        {
            Id = Guid.NewGuid().ToString();
        }

        public TemplateItemDTO(string id, bool? isWireFrame, bool? isFlexItem, bool? isFlexRow, bool? isFlexGrow
                             , bool? isFlexBasis, bool? isShadowBox, bool? showHeader, string? header, string? headerStyle
                             , bool? showTable, bool? denseTable, bool? hoverTable, bool? borderTable, bool? stripeTable
                             , string? headerTable, string? cTableUrl, string? rTableUrl, string? uTableUrl, string? dTableUrl
                             , string? tableEntityType
                             , string? templateItem)
        {
            Id = id;
            IsWireFrame = isWireFrame;
            IsFlexItem = isFlexItem;
            IsFlexRow = isFlexRow;
            IsFlexGrow = isFlexGrow;
            IsFlexBasis = isFlexBasis;
            IsShadowBox = isShadowBox;
            ShowHeader = showHeader;
            Header = header;
            HeaderStyle = headerStyle;
            ShowTable = showTable;
            DenseTable = denseTable;
            HoverTable = hoverTable;
            BorderTable = borderTable;
            StripeTable = stripeTable;
            HeaderTable = headerTable;
            CTableUrl = cTableUrl;
            RTableUrl = rTableUrl;
            UTableUrl = uTableUrl;
            DTableUrl = dTableUrl;
            TableEntityType = tableEntityType;
            TemplateItem = templateItem;
        }

        public string Id { get; set; }
        public List<TemplateItemComponent>? Childs { get; set; }

        public string GetCss()
        {
            if (IsWireFrame == true) return GetTemplateCssWireFrame();
            return GetTemplateCssFlexItem();
        }
        public string GetTemplateCssWireFrame()
        {
            return $"{GetWireFrameCss()} {GetFlexDirectionCss()}";
        }
        public string GetTemplateCssFlexItem()
        {
            return $"{GetFlexItemCss()} {GetFlexDirectionCss()} {GetFlexGrowCss()} {GetFlexBasisCss()} {GetShadowBoxCss()}";
        }

        public bool? IsWireFrame { get; set; } = false;
        public string GetWireFrameCss()
        {
            if (IsWireFrame ?? false) return "flex-wireframe";
            return "";
        }
        public bool? IsFlexItem { get; set; } = false;
        public string GetFlexItemCss()
        {
            if (IsFlexItem ?? false) return "flex-item";
            return "";
        }

        public bool? IsFlexRow { get; set; } = false;
        public string GetFlexDirectionCss()
        {
            if (IsFlexRow ?? false) return "flex-row";
            return "flex-column";
        }

        public bool? IsFlexGrow { get; set; } = false;
        public string GetFlexGrowCss()
        {
            if (IsFlexGrow ?? false) return "flex-grow-true";
            return "flex-grow-false";
        }
        public bool? IsFlexBasis { get; set; } = false;
        public string GetFlexBasisCss()
        {
            if (IsFlexGrow ?? false) return "flex-basis-true";
            return "flex-basis-false";
        }

        public bool? IsShadowBox { get; set; } = false;
        public string GetShadowBoxCss()
        {
            if (IsShadowBox ?? false) return "shadow-box";
            return "";
        }

        public bool IsTable()
        {
            if (TemplateItem is null || TemplateItem.Equals(TemplateItemTable))
            {
                return true;
            }
            return false;
        }

        public bool? ShowHeader { get; set; } = false;
        public string? Header { get; set; } = "";
        public string? HeaderStyle { get; set; } = "";
        public bool? ShowTable { get; set; } = false;
        public bool? DenseTable { get; set; } = false;
        public bool? HoverTable { get; set; } = false;
        public bool? BorderTable { get; set; } = false;
        public bool? StripeTable { get; set; } = false;
        public string? HeaderTable { get; set; } = "";
        public string? CTableUrl { get; set; } = "";
        public string? RTableUrl { get; set; } = "";
        public string? UTableUrl { get; set; } = "";
        public string? DTableUrl { get; set; } = "";
        public string? TableEntityType { get; set; } = "";
        public string? TemplateItem { get; set; } = "";
    }
}