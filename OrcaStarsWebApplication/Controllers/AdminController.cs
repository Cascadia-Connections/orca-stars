using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OrcaStarsWebApplication.Models;
using OrcaStarsWebApplication.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcaStarsWebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminController(IWebHostEnvironment HostEnv)
        {
            webHostEnvironment = HostEnv;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ApplicationViewModel avm)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (avm.BusinessLogo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images"); //images location as string format
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + avm.BusinessLogo.FileName; //make sure uploaded file is unique
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName); //combining uploads folder and unique file name to create it files path
                    avm.BusinessLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                    avm.BusinessLogoHolder = uniqueFileName;
                }
                if (avm.StoreLogo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images"); //images location as string format
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + avm.StoreLogo.FileName; //make sure uploaded file is unique
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName); //combining uploads folder and unique file name to create it files path
                    avm.StoreLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                    avm.StoreLogoHolder = uniqueFileName;
                }

                return RedirectToAction("Confirm", avm);
            }
            //If the model didn't work, don't leave
            return View();

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
