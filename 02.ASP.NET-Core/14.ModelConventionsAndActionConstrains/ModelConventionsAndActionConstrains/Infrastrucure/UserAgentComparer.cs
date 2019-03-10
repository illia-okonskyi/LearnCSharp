using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ModelConventionsAndActionConstrains.Infrastrucure
{
    public class UserAgentComparer
    {
        public bool ContainsString(HttpRequest request, string agent)
        {
            string searchTerm = agent.ToLower();
            return request.Headers["User-Agent"].Any(h => h.ToLower().Contains(searchTerm));
        }
    }
}
