using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Czytnik.Helpers
{
    [HtmlTargetElement("stars")]
    public class StarsHelper : TagHelper
    {
        public int starsCount { get; set; }
        public string parentClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var className = $"stars-rating stars-rating--{starsCount} {parentClass}";
            output.TagName = "div";
            output.Attributes.Add("class", className);
        }
    }
}
