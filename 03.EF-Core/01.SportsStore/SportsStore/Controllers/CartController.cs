using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using SportsStore.Models;
using SportsStore.Infrastructure;

namespace SportsStore.Controllers
{
    [ViewComponent(Name = "Cart")]
    public class CartController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        public CartController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(GetCart());
        }

        [HttpPost]
        public IActionResult AddToCart(Product product, string returnUrl)
        {
            SaveCart(GetCart().AddItem(product, 1));
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(long productId, string returnUrl)
        {
            SaveCart(GetCart().RemoveItem(productId));
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.Lines = GetCart().Selections.Select(s => new OrderLine {
                ProductId = s.ProductId,
                Quantity = s.Quantity
            }).ToArray();
            _ordersRepository.AddOrder(order);

            SaveCart(new Cart());
            return RedirectToAction(nameof(Completed));
        }
        public IActionResult Completed() => View();

        private Cart GetCart() => HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

        private void SaveCart(Cart cart) => HttpContext.Session.SetJson("cart", cart);

        public IViewComponentResult Invoke(ISession session)
        {
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<Cart>(ViewData, session.GetJson<Cart>("cart"))
            };
        }
    }
}
