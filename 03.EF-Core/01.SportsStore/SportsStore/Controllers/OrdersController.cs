using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IRepository _productRepository;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IRepository productRepository,
            IOrdersRepository ordersRepository)
        {
            _productRepository = productRepository;
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index() => View(_ordersRepository.Orders);

        public IActionResult EditOrder(long id)
        {
            var products = _productRepository.AllProducts;
            var order = id == 0 ? new Order() : _ordersRepository.GetOrder(id);
            var linesMap = order.Lines?.ToDictionary(l => l.ProductId) ??
                new Dictionary<long, OrderLine>();
            ViewBag.Lines = products.Select(p => {
                return linesMap.ContainsKey(p.Id)
                    ? linesMap[p.Id]
                    : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 };
                });
            return View(order);
        }

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            // Store only exising lines and lines with quantity > 0
            order.Lines = order.Lines.Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToList();
            if (order.Id == 0)
            {
                _ordersRepository.AddOrder(order);
            }
            else
            {
                _ordersRepository.UpdateOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            _ordersRepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
