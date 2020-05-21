using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OrcaStarsWebApplication.Models;
using OrcaStarsWebApplication.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcaStarsWebApplication.Controllers
{
    public class AdminController : Controller
    {
        [Obsolete]
        public IHostingEnvironment HostingEnvironment { get; }

        [Obsolete]
        public AdminController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

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
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (avm.Logo != null)
                {
                    string uploadsFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + avm.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    avm.Logo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                return View(avm);
            }



            return View(avm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
