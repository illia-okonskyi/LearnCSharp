using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
