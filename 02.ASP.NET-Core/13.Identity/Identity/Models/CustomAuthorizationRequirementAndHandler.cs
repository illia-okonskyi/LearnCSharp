using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Models
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public CustomAuthorizationRequirement(params string[] blockedUsers)
        {
            BlockedUsers = blockedUsers;
        }

        public string[] BlockedUsers { get; set; }
    }

    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthorizationRequirement requirement)
        {
            var userName = context.User.Identity?.Name;
            if (userName == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var isUserBlocked = requirement.BlockedUsers.Any(blockedUser => {
                return userName.Equals(blockedUser, StringComparison.OrdinalIgnoreCase);
            });
            if (isUserBlocked)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
