using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.Infrastructure.TagHelpers
{
    public class FormTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public FormTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContextData);
            output.Attributes.SetAttribute(
                "action",
                urlHelper.Action(
                    Action ?? ViewContextData.RouteData.Values["action"].ToString(),
                    Controller ?? ViewContextData.RouteData.Values["controller"].ToString()
                    ));
        }
    }
}
