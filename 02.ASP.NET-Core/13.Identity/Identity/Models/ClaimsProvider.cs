using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Identity.Models
{
    // NOTE: It seems that there can be only one IClaimsTransformation service (internally in
    //       ASP.NET Core Identity) and all claims must be populated through it.
    public class ClaimsProvider : IClaimsTransformation
    {
        private static Claim CreateClaim(string type, string value) =>
            new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (!(principal?.Identity is ClaimsIdentity identity) ||
                !identity.IsAuthenticated ||
                identity.Name == null)
            {
                return Task.FromResult(principal);
            }

            if (!principal.HasClaim(c => c.Type == ClaimTypes.PostalCode))
            {
                // Imitate obtaining the location claims
                if (identity.Name.ToLower() == "alice")
                {
                    identity.AddClaims(new Claim[] {
                            CreateClaim(ClaimTypes.PostalCode, "DC 20500"),
                            CreateClaim(ClaimTypes.StateOrProvince, "DC"),
                            CreateClaim(CustomClaimTypes.UserComment, "User comment for Alice")
                        });
                }
                else
                {
                    identity.AddClaims(new Claim[] {
                            CreateClaim(ClaimTypes.PostalCode, "NY 10036"),
                            CreateClaim(ClaimTypes.StateOrProvince, "NY"),
                            CreateClaim(CustomClaimTypes.UserComment, "User comment for non-Alice")
                        });
                }
            }

            return Task.FromResult(principal);
        }
    }
}
