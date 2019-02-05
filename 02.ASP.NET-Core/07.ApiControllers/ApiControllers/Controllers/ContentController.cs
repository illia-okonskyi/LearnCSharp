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

    }
}
