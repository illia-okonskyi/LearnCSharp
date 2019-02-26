using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Identity.Models;

namespace Identity.Infrastructure
{
    // NOTE: The UserValidator<TUser> class implements default validation policy and can be used to 
    //       create own custom validator which is based on the default.
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> userManager, AppUser user)
        {
            if (!user.Email.ToLower().EndsWith("@example.com"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailDomainError",
                    Description = "Only example.com email addresses are allowed"
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
