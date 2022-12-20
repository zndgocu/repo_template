using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems
{
    public class TemplateItemComponent
    {
        public TemplateItemComponent(string id)
        {
            this.Id = id;
        }

        public string? Id { get; set;}
        public TemplateItem? Component { get; set; }
    }
}