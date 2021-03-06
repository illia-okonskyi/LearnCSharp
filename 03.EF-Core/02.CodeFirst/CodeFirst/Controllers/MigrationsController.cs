﻿using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodeFirst.Controllers
{
    public class MigrationsController : Controller
    {
        private readonly MigrationsManager _migrationsManager;

        public MigrationsController(MigrationsManager migrationsManager)
        {
            _migrationsManager = migrationsManager;
        }

        public IActionResult Index(string context)
        {
            ViewBag.Context = _migrationsManager.ContextName = context ?? _migrationsManager.ContextNames.First();
            return View(_migrationsManager);
        }

        [HttpPost]
        public IActionResult Migrate(string context, string migration)
        {
            _migrationsManager.ContextName = context;
            _migrationsManager.Migrate(context, migration);
            return RedirectToAction(nameof(Index), new { context });
        }

        [HttpPost]
        public IActionResult Seed(string context)
        {
            _migrationsManager.ContextName = context;
            SeedData.Seed(_migrationsManager.Context);
            return RedirectToAction(nameof(Index), new { context });
        }

        [HttpPost]
        public IActionResult Clear(string context)
        {
            _migrationsManager.ContextName = context;
            SeedData.ClearData(_migrationsManager.Context);
            return RedirectToAction(nameof(Index), new { context });
        }
    }
}
