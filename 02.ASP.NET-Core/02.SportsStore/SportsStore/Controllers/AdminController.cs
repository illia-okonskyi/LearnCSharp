﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;

        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index() => View(_productRepository.Products);

        public ViewResult Edit(int productId)
        {
            return View(_productRepository.Products.FirstOrDefault(p => p.Id == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _productRepository.SaveProduct(product);
            TempData["message"] = $"{product.Name} has been saved";
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Create() => View(nameof(Edit), new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deletedProduct = _productRepository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
