using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.Infrastructure.TagHelpers
{
    // NOTE: The name of the tag helper combines the name of the element it transforms followed by
    //       TagHelper. In the case of the example, the class name ButtonTagHelper tells MVC that
    //       this is a tag helper that operates on button elements.
    // NOTE: Tag helpers are applied to all the elements of a given type, Tag helper restrictions
    ///      are applied using the HtmlTargetElement attribute
    // NOTE: When a tag helper is being used, MVC inspects the properties it defines and sets the
    //       value of any whose name matches attributes applied to the HTML element.
    //       The name of the attribute is automatically converted from the default HTML style,
    //       bs-button-color, to the C# style, BsButtonColor. You can use any attribute prefix
    //       except asp- (which Microsoft uses) and data- (which is reserved for custom attributes
    //       that are sent to the client).
    //       The HtmlAttributeName attribute can be used to specify the HTML attribute that the
    //       property will represent.
    // NOTE: If you do need to apply multiple tag helpers, then you can control the sequence in
    //       which they execute by setting the Order property, which is inherited from the TagHelper
    //       base class.
    [HtmlTargetElement("button", Attributes = "bs-button-color", ParentTag = "form")]
    [HtmlTargetElement("a", Attributes = "bs-button-color", ParentTag = "form")]
    public class ButtonTagHelper : TagHelper
    {
        public string BsButtonColor { get; set; }

        // NOTE: Asynchronous tag helpers can be created by overriding the ProcessAsync method
        //       instead of the Process
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"btn btn-{BsButtonColor}");
        }
    }
}
