using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers
{
    public class RelatedDataController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IGenericRepository<ContactDetails> _detailsRepository;
        private readonly IGenericRepository<ContactLocation> _locationsRepository;

        public RelatedDataController(ISupplierRepository supplierRepository,
            IGenericRepository<ContactDetails> detailsRepository,
            IGenericRepository<ContactLocation> locationsRepository)
        {
            _supplierRepository = supplierRepository;
            _detailsRepository = detailsRepository;
            _locationsRepository = locationsRepository;
        }

        public IActionResult Index() => View(_supplierRepository.GetAll());
        public IActionResult Contacts() => View(_detailsRepository.GetAll());
        public IActionResult Locations() => View(_locationsRepository.GetAll());
    }
}
