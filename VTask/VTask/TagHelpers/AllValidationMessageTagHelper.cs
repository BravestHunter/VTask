using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VTask.TagHelpers
{
    [HtmlTargetElement("ul", Attributes = AttributeName)]
    public class AllValidationMessageTagHelper : TagHelper
    {
        private const string AttributeName = "asp-validation-for-all";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            
            if (ViewContext == null)
            {
                return;
            }

            ModelStateEntry? entry;
            ViewContext.ViewData.ModelState.TryGetValue("All", out entry);

            if (entry != null && entry.Errors.Count > 0)
            {
                foreach (var error in entry.Errors)
                {
                    output.Content.AppendFormat("<li>{0}</li>", error.ErrorMessage);
                }
            }
        }
    }
}
