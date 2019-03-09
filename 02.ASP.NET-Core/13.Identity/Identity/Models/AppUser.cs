using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public enum City
    {
        None,
        London,
        Paris,
        Chicago
    }

    public enum Qualification
    {
        None,
        Basic,
        Advanced
    }

    public class AppUser : IdentityUser
    {
        public City City { get; set; }
        public Qualification Qualification { get; set; }
    }
}
