using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Models
{
    public class DocumentAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool AllowAuthors { get; set; }
        public bool AllowEditors { get; set; }
    }

    public class DocumentAuthorizationHandler : AuthorizationHandler<DocumentAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DocumentAuthorizationRequirement requirement)
        {
            var doc = context.Resource as ProtectedDocument;
            var user = context.User.Identity.Name;
            if (doc == null || user == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            StringComparison compare = StringComparison.OrdinalIgnoreCase;
            if (requirement.AllowAuthors && doc.Author.Equals(user, compare))
            {
                context.Succeed(requirement);
            }
            else if (requirement.AllowEditors && doc.Editor.Equals(user, compare))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }

}
