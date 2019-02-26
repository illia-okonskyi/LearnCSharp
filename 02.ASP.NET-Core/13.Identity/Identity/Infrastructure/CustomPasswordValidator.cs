using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Identity.Models;

namespace Identity.Infrastructure
{
    // NOTE: The PasswordValidator<TUser> class implements default validation policy and can be used
    //       to create own custom validator which is based on the default.
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(
            UserManager<AppUser> userManager,
            AppUser user,
            string password)
        {
            var errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contain username"
                });
            }
            if (password.Contains("12345"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsSequence",
                    Description = "Password cannot contain numeric sequence"
                });
            }

            if (errors.Count != 0) {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
