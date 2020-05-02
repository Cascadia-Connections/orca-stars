﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrcaStarsWebApplication.Models;
using OrcaStarsWebApplication.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcaStarsWebApplication.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ApplicationViewModel avm)
        {
            return RedirectToAction("Confirm", avm );
        }

        [HttpGet]
        public IActionResult Confirm(ApplicationViewModel avm)
        {
            return View(avm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
