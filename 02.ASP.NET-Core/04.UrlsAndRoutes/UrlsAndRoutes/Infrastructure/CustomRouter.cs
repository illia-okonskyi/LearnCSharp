using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace UrlsAndRoutes.Infrastructure
{
    // Ingoing URLs: Redirrects `legacyUrls` to the LegacyController
    // Outcoming URLs: if there is a "legacyUrl" route part and if it's value is in `legacyUrls`
    //                 returns the "legacyUrl" route part as a full URL
    public class CustomRouter : IRouter
    {
        private readonly string[] _legacyUrls;
        private readonly IRouter _mvcRouter;
        public CustomRouter(IServiceProvider services, params string[] legacyUrls)
        {
            _legacyUrls = legacyUrls;
            _mvcRouter = services.GetRequiredService<MvcRouteHandler>();
        }

        // `RouteAsync` Is used to route incoming URLs
        public async Task RouteAsync(RouteContext context)
        {
            string requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if (!_legacyUrls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                return;
            }

            context.RouteData.Values["controller"] = "Legacy";
            context.RouteData.Values["action"] = "GetLegacyUrl";
            context.RouteData.Values["legacyUrl"] = requestedUrl;
            await _mvcRouter.RouteAsync(context);
        }

        // `GetVirtualPath` Is used to route outgoing URLs
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            if (!context.Values.ContainsKey("legacyUrl"))
            {
                return null;
            }

            var legacyUrl = context.Values["legacyUrl"] as string;
            if (!_legacyUrls.Contains(legacyUrl))
            {
                return null;
            }

            return new VirtualPathData(this, legacyUrl);
        }
    }
}
