using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ModelConventionsAndActionConstrains.Infrastrucure
{
    public class UserAgentAttribute : Attribute, IActionConstraint
    {
        private readonly string _userAgentSubstring;
        public UserAgentAttribute(string userAgentSubstring)
        {
            _userAgentSubstring = userAgentSubstring.ToLower();
        }

        public int Order { get; set; } = 0;

        public bool Accept(ActionConstraintContext context)
        {
            // Other actions which can be suitable for handling the request are listed in 
            // context.Candidates property

            var userAgentHeaderValues = context.RouteContext.HttpContext.Request.Headers["User-Agent"];
            return userAgentHeaderValues.Any(h => h.ToLower().Contains(_userAgentSubstring));
        }
    }
}
