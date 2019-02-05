using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using ApiControllers.Models;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository _repository;

        public ReservationController(IRepository repo) => _repository = repo;

        [HttpGet]
        public IEnumerable<Reservation> Get() => _repository.Reservations;

        [HttpGet("{id}")]
        public Reservation Get(int id) => _repository[id];

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            _repository.AddReservation(new Reservation
            {
                ClientName = res.ClientName,
                Location = res.Location
            });

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) =>
            _repository.UpdateReservation(res);

        // NOTE: JSON Patch is a standard used to modify contents of the JSON document.
        //       Reference: https://tools.ietf.org/html/rfc6902
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _repository.DeleteReservation(id);
    }
}
