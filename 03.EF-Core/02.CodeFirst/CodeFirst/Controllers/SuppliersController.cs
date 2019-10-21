using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    public class SuppliersController : Controller
    {
        private ISupplierRepository _supplierRepository;
        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public IActionResult Index()
        {
            ViewBag.SupplierEditId = TempData["SupplierEditId"];
            ViewBag.SupplierCreateId = TempData["SupplierCreateId"];
            ViewBag.SupplierRelationshipId = TempData["SupplierRelationshipId"];
            return View(_supplierRepository.GetAll());
        }

        public IActionResult Edit(long id)
        {
            TempData["SupplierEditId"] = id;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create(long id)
        {
            TempData["SupplierCreateId"] = id;
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Update(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Change(long id)
        {
            TempData["SupplierRelationshipId"] = id;
            return RedirectToAction(nameof(Index));
        }

        // This approach works-around EF Core change detection system
        // The more siplier way of doing this is to use direct direction of the one-to-many
        // relation like this:
        // public IActionResult Change(long Id, Product[] products)
        // {
        //     dbContext.Products.UpdateRange(products.Where(p => p.SupplierId != Id));
        //     dbContext.SaveChanges();
        //     return RedirectToAction(nameof(Index));
        // }
        [HttpPost]
        public IActionResult Change(Supplier supplier)
        {
            var changedProducts = supplier.Products.Where(p => p.SupplierId != supplier.Id);
            if (changedProducts.Count() > 0)
            {
                var allSuppliers = _supplierRepository.GetAll().ToArray();
                var currentSupplier = allSuppliers.First(s => s.Id == supplier.Id);
                foreach (var p in changedProducts)
                {
                    var newSupplier = allSuppliers.First(s => s.Id == p.SupplierId);
                    newSupplier.Products = newSupplier.Products
                        .Append(currentSupplier.Products.First(op => op.Id == p.Id)).ToArray();
                    _supplierRepository.Update(newSupplier);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
