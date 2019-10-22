using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    public class ProductShipmentViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Shipment> Shipments { get; set; }
    }

    public class Many2ManyController : Controller
    {
        private readonly EfDbContext _dbContext;
        public Many2ManyController(EfDbContext dbContext) => _dbContext = dbContext;

        public IActionResult Index()
        {
            return View(new ProductShipmentViewModel
            {
                Products = _dbContext.Products
                    .Include(p => p.ProductShipments)
                    .ThenInclude(ps => ps.Shipment).ToArray(),
                Shipments = _dbContext.Set<Shipment>()
                    .Include(s => s.ProductShipments)
                    .ThenInclude(ps => ps.Product).ToArray()
            });
        }

        public IActionResult EditShipment(long id)
        {
            ViewBag.Products = _dbContext.Products.Include(p => p.ProductShipments);
            return View("ShipmentEditor", _dbContext.Set<Shipment>().Find(id));
        }

        [HttpPost]
        public IActionResult UpdateShipment(long id, long[] pids)
        {
            var shipment = _dbContext.Set<Shipment>()
                .Include(s => s.ProductShipments)
                .First(s => s.Id == id);
            shipment.ProductShipments = pids.Select(pid => new ProductShipmentJunction
            {
                ShipmentId = id,
                ProductId = pid
            }).ToList();
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
