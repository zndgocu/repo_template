using EntityContext.Fms;

namespace blazor_wasm.Client.Pages.TemplatePages
{
    public static class TemplatePagesConst
    {
        public const string FLEX_DIRECTION_ROW = "ROW";
        public const string FLEX_DIRECTION_COL = "COLUMN";

        public const string URL_GET_ENTITY_SPEC = "entity-spec/get/entity-type-collection";
        public static Type GetEntitySpecType(){
            return typeof(EntitySpec);
        }
        
        /// <summary>
        /// http://localhost:7268/entity-spec/get/entity-type-collection?entityType=menu_item'
        /// http://localhost:7268/entity-spec/get/entity-type-collection?
        /// </summary>
        /// <param name="str">varFullName=value</param>
        /// <returns></returns>
        public static string GetUrlEntitySpec(string str)
        {
            //http://localhost:7268/entity-spec/get/entity-type-collection?entityType=menu_item'
            return $"/{URL_GET_ENTITY_SPEC}?entityType={str}";
        }
    }
}