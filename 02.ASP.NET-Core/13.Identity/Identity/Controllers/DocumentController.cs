using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.Models;

namespace Identity.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private ProtectedDocument[] _protectedDocuments = new ProtectedDocument[] {
            new ProtectedDocument { Title = "Q3 Budget", Author = "Alice", Editor = "Joe"},
            new ProtectedDocument { Title = "Project Plan", Author = "Bob", Editor = "Alice"}
        };

        private readonly IAuthorizationService _authorizationService;

        public DocumentController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public ViewResult Index() => View(_protectedDocuments);

        public async Task<IActionResult> Edit(string title)
        {
            var doc = _protectedDocuments.FirstOrDefault(d => d.Title == title);
            var authorizationResult = await _authorizationService.AuthorizeAsync(
                User, doc, "AuthorsAndEditors");
            if (!authorizationResult.Succeeded)
            {
                return new ChallengeResult();
            }

            return View(nameof(Index), doc);
        }
    }
}
