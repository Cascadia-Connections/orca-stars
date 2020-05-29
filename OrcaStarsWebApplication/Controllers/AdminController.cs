using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
                string uniqueBusinessFileName = null;
                string uniqueStoreFileName = null;

                avm.BusinessLogoHolder = "images/orcastarsImages/defaultBusinessStorelogo.png";
                avm.StoreLogoHolder = "images/orcastarsImages/defaultBusinessStorelogo.png";
                
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/uploads"); //images location as string format

                if (avm.BusinessLogo != null) 
                {
                    uniqueBusinessFileName = Guid.NewGuid().ToString() + "_" + avm.BusinessLogo.FileName; //make sure uploaded file is unique
                    string filePath = Path.Combine(uploadsFolder, uniqueBusinessFileName); //combining uploads folder and unique file name to create it files path
                    avm.BusinessLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                    avm.BusinessLogoHolder = "images/uploads/" + uniqueBusinessFileName;
                }

                if (avm.StoreLogo != null) 
                {
                    uniqueStoreFileName = Guid.NewGuid().ToString() + "_" + avm.StoreLogo.FileName; //make sure uploaded file is unique
                    string filePath = Path.Combine(uploadsFolder, uniqueStoreFileName); //combining uploads folder and unique file name to create it files path
                    avm.StoreLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                    avm.StoreLogoHolder = "images/uploads/" + uniqueStoreFileName;
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
