using System.Text.Json.Serialization;
using EntityContext.Fms.Wrapper;

namespace EntityContext.Fms
{
    public class MenuItem : FmsWrapper
    {
        public MenuItem() : base(typeof(MenuItem))
        {
            Id = Id ?? "";
        }


        protected override List<string>? KeyColumns => new List<string>(){
            "id"
        };
        protected override List<string>? BindColumns => new List<string>(){
            "is_template_page"
        };

        [JsonInclude]
        public string Id { get; set; }
        [JsonInclude]
        public string? ParentId { get; set; }
        [JsonInclude]
        public bool? IsTemplatePage { get; set; }
        [JsonInclude]
        public string? Href { get; set; }
        [JsonInclude]
        public int? Prio { get; set; }
        [JsonInclude]
        public string DisplayName { get; set; } = "";

        [JsonIgnore]
        public List<MenuItem>? Childs { get; set; }
    }
}