﻿using Microsoft.AspNetCore.Mvc;
using ModelFirst.Models.Manual;

namespace ModelFirst.Controllers
{
    public class ManualController : Controller
    {
        private readonly ManualContext _context;

        public ManualController(ManualContext context) => _context = context;

        public IActionResult Index() => View(_context.Shoes);
    }
}
