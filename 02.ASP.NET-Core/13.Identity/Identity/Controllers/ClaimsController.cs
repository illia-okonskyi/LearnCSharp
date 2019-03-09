using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    // NOTE: A claim is a piece of information about the user, along with some information about
    //      where the information came from.
    public class ClaimsController : Controller
    {
        [Authorize]
        public ViewResult Index() => View(User?.Claims);
    }
}
