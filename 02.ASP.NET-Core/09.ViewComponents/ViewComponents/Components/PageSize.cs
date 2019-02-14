using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewComponents.Components
{
    public class PageSize : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://apress.com");
            return View(response.Content.Headers.ContentLength);
        }
    }
}
