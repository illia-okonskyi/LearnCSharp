using ApiControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        // NOTE: If the action method returns a string, the string is sent unmodified to the client,
        // and the Content-Type header of the response is set to text/plain
        [HttpGet("string")]
        public string GetString() => "This is a string response";

        // NOTE: Default behavior for all return types except string is to format it to JSON
        [HttpGet("objectAsJson")]
        [Produces("application/json")]
        public Reservation GetObjectAsJson() => new Reservation
        {
            Id = 100,
            ClientName = "Joe",
            Location = "Board Room"
        };

        [HttpGet("objectAsXml")]
        [Produces("application/xml")]
        public Reservation GetObjectAsXml() => new Reservation
        {
            Id = 100,
            ClientName = "Joe",
            Location = "Board Room"
        };

        // NOTE: FormatFilter attribute installs filter which does next:
        //       - looks for a routing segment variable called format
        //       - gets the shorthand value that it contains,
        //       - and retrieves the associated data format from the application configuration.
        //         This format is then used for the response.
        //       If there is no routing data available, then the query string is inspected as well.
        // NOTE: You don’t have to use the Produces attribute in conjunction with the FormatFilter
        //       attribute, but if you do, only requests that specify formats for which the
        //       Produces attribute has been configured will work
        [HttpGet("object/{format?}")]
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public Reservation GetObject() => new Reservation
        {
            Id = 100,
            ClientName = "Joe",
            Location = "Board Room"
        };
    }
}
