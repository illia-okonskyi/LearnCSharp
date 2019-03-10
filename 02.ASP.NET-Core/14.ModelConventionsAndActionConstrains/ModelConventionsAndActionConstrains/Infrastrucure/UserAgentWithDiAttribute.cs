using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.DependencyInjection;

namespace ModelConventionsAndActionConstrains.Infrastrucure
{
    public class UserAgentWithDiAttribute : Attribute, IActionConstraintFactory
    {
        private readonly string _userAgentSubstring;

        public UserAgentWithDiAttribute(string userAgentSubstring)
        {
            _userAgentSubstring = userAgentSubstring;
        }

        public bool IsReusable => false;

        public IActionConstraint CreateInstance(IServiceProvider services)
        {
            var comparer = services.GetService<UserAgentComparer>();
            return new UserAgentConstraint(comparer, _userAgentSubstring);
        }

        private class UserAgentConstraint : IActionConstraint
        {
            private readonly UserAgentComparer _comparer;
            private readonly string _userAgentSubstring;
            public UserAgentConstraint(UserAgentComparer comparer, string userAgentSubstring)
            {
                _comparer = comparer;
                _userAgentSubstring = userAgentSubstring;
            }

            public int Order { get; set; } = 0;

            public bool Accept(ActionConstraintContext context)
            {
                var request = context.RouteContext.HttpContext.Request;
                return _comparer.ContainsString(request, _userAgentSubstring);
            }
        }
    }
}
