using System.Text.Json.Serialization;
using EntityContext.Fms.Wrapper;

namespace EntityContext.Fms
{
    public class EntitySpec : FmsWrapper
    {
        public EntitySpec() : base(typeof(EntitySpec))
        {
            EntityType = "";
            EntityItemCd = "";
            EntityItemVal = "";
        }


        protected override List<string>? KeyColumns => new List<string>(){
            "entity_type", "entity_item_cd", "entity_item_val",
        };
        protected override List<string>? BindColumns => new List<string>(){

        };

        [JsonInclude]
        public string EntityType { get; set; }
        [JsonInclude]
        public string EntityItemCd { get; set; }
        [JsonInclude]
        public string EntityItemVal { get; set; }
    }
}