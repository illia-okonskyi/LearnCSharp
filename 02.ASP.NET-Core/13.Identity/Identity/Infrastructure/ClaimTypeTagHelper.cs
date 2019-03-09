using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Identity.Models;

namespace Identity.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-claim-type")]
    public class ClaimTypeTagHelper : TagHelper
    {
        [HtmlAttributeName("identity-claim-type")]
        public string ClaimType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var claimTypesFields = typeof(ClaimTypes).GetFields()
                .Concat(typeof(CustomClaimTypes).GetFields());
            var targetClaimTypeField = claimTypesFields.SingleOrDefault(
                field => field.GetValue(null).ToString() == ClaimType);

            var claimTypeString = targetClaimTypeField?.Name ?? ClaimType.Split('/', '.').Last();
            output.Content.SetContent(claimTypeString);
        }
    }
}
