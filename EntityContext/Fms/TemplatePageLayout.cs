using System.Text.Json.Serialization;
using EntityContext.Fms.Wrapper;

namespace EntityContext.Fms
{
    public class TemplatePageLayout : FmsWrapper
    {
        public TemplatePageLayout() : base(typeof(TemplatePageLayout))
        {
            Id = Id ?? "";
        }


        protected override List<string>? KeyColumns => new List<string>(){
            "id"
        };

        protected override List<string>? BindColumns => new List<string>(){
            "is_wire_frame", 
            "is_flex_item",
            "is_flex_row",
            "is_flex_grow",
            "is_flex_basis",
            "is_shadow_box",
            "show_header",
            "show_table",
            "dense_table",
            "hover_table",
            "border_table",
            "stripe_table",
            "header_table",
            "template_item"
        };

        [JsonInclude]
        public string Id { get; set; }
        [JsonInclude]
        public string? ParentId { get; set; }
        [JsonInclude]
        public bool? IsWireFrame { get; set; }
        [JsonInclude]
        public bool? IsFlexItem { get; set; }
        [JsonInclude]
        public bool? IsFlexRow { get; set; }
        [JsonInclude]
        public bool? IsFlexGrow { get; set; }
        [JsonInclude]
        public bool? IsFlexBasis { get; set; }
        [JsonInclude]
        public bool? IsShadowBox { get; set; }
        [JsonInclude]
        public bool? ShowHeader { get; set; }
        [JsonInclude]
        public string? Header { get; set; }
        [JsonInclude]
        public string? HeaderStyle { get; set; }
        [JsonInclude]
        public bool? ShowTable { get; set; }


        [JsonInclude]
        public bool? DenseTable { get; set; }
        [JsonInclude]
        public bool? HoverTable { get; set; }
        [JsonInclude]
        public bool? BorderTable { get; set; }
        [JsonInclude]
        public bool? StripeTable { get; set; }
        [JsonInclude]
        public string? HeaderTable { get; set; }
        [JsonInclude]
        public string? CTableUrl { get; set; }
        [JsonInclude]
        public string? RTableUrl { get; set; }
        [JsonInclude]
        public string? UTableUrl { get; set; }
        [JsonInclude]
        public string? DTableUrl { get; set; }
        [JsonInclude]
        public string? TableEntityType { get; set; }
        [JsonInclude]
        public string? TemplateItem { get; set; }

        // [JsonInclude]
        // [JsonPropertyName("id")]
        // public string Id { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("parent_id")]
        // public string? ParentId { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("is_wire_frame")]
        // public bool? IsWireFrame { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("is_flex_item")]
        // public bool? IsFlexItem { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("is_flex_row")]
        // public bool? IsFlexRow { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("is_flex_grow")]
        // public bool? IsFlexGrow { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("is_flex_basis")]
        // public bool? IsFlexBasis { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("is_shadow_box")]
        // public bool? IsShadowBox { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("show_header")]
        // public bool? ShowHeader { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("header")]
        // public string? Header { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("header_style")]
        // public string? HeaderStyle { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("show_table")]
        // public bool? ShowTable { get; set; }


        // [JsonInclude]
        // [JsonPropertyName("dense_table")]
        // public bool? DenseTable { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("hover_table")]
        // public bool? HoverTable { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("border_table")]
        // public bool? BorderTable { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("stripe_table")]
        // public bool? StripeTable { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("header_table")]
        // public string? HeaderTable { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("c_table_url")]
        // public string? CTableUrl { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("r_table_url")]
        // public string? RTableUrl { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("u_table_url")]
        // public string? UTableUrl { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("d_table_url")]
        // public string? DTableUrl { get; set; }
        // [JsonInclude]
        // [JsonPropertyName("table_entity_type")]
        // public string? TableEntityType { get; set; }
    }
}