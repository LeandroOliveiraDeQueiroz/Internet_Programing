﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Internet_Programing.Models;
using Internet_Programing.Data;
using Microsoft.EntityFrameworkCore;

namespace Internet_Programing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoppingDbContext _context;

        public HomeController(ILogger<HomeController> logger, ShoppingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
