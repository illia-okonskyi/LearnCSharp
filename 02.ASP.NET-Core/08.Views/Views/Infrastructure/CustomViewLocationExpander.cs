using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Views.Infrastructure
{
    // NOTE: Individual IViewLocationExpanders are invoked in two steps:
    //       1) PopulateValues(ViewLocationExpanderContext) is invoked and each expander adds values
    //          that it would later consume as part of ExpandViewLocations. The populated values are
    //          used to determine a cache key - if all values are identical to the last time
    //          PopulateValues was invoked, the cached result is used as the view location.
    //       2) If no result was found in the cache or if a view was not found at the cached 
    //          location, ExpandViewLocations is invoked to determine all potential paths for a
    //          view.
    // NOTE: In ExpandViewLocations the placeholder {0} is used to refer to the name of the action 
    //       method, and {1} is the placeholder for the controller name.
    public class CustomViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // For this implementation is not required
        }

        // As example, generates next values (requested view is CustomViewLocationExpander):
        // - /Views/Home/CustomViewLocationExpander.cshtml
        // - /Views/Common/CustomViewLocationExpander.cshtml
        // - /Pages/Common/CustomViewLocationExpander.cshtml
        // - /Views/Legacy/Home/CustomViewLocationExpander/View.cshtml
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            foreach (string location in viewLocations)
            {
                yield return location.Replace("Shared", "Common");
            }
            yield return "/Views/Legacy/{1}/{0}/View.cshtml";
        }
    }
}
