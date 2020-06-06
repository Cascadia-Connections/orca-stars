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
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcaStarsWebApplication.Controllers
{

    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private BitDataContext _db;
        public AdminController(BitDataContext db) { _db = db; }

        public AdminController(IWebHostEnvironment HostEnv)
        {
            webHostEnvironment = HostEnv;
        }


        //CREATE //

        [HttpGet] //THIS IS THE FORM
        public IActionResult Index()
        {
            return View();
        }

        //CREATE //

        [HttpPost] //THIS PUSHES FORM DATA TO DATA BASE
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

                Business business = new Business
                {
                 
                    Name = avm.BusinessName,
                    Description = avm.Description,
                    PhoneNumber = avm.BusinessPhone,
                    Address1 = avm.AddressLine1,
                    Address2 = avm.AddressLine2,
                    City = avm.City,
                    State = avm.State,
                    Country = avm.Country,
                    ZipCode = avm.Zip,
                    Website = avm.Website,
                    Category = avm.Category,
                    Hours = avm.Hours,
                    Social = avm.SocialMedia,
                    Logo = avm.BusinessLogo,
                    StoreFront = avm.StoreLogo
                };

                BusinessContact businessContact = new BusinessContact
                {
                    FirstName = avm.FirstName,
                    LastName = avm.LastName,
                    PhoneNumber = avm.PhoneNumber, //contact view model phone number?
                    Email = avm.Email
                };

                _db.Businesses.Add(business);
                _db.Contacts.Add(businessContact);
                _db.SaveChanges();

                return RedirectToAction("Confirm", avm); //TAKES YOU TO BUSINESS INFO CONFIRMATION PAGE
            }
            //If the model didn't work, return View
            return View();
        }

        [HttpGet] //DISPLAYS BUSINESS INFO
        public IActionResult Confirm(ApplicationViewModel avm)
        {
            return View(avm);
        }


        // READ AND SEARCH //

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(ApplicationViewModel avm)
        {
            //Full Collection Search - start with entire collection
            IQueryable<Business> foundBusinesses = _db.Businesses.OrderBy(b => b.Id);

            //Partial Title Search
            if (avm.BusinessName != null)
            {
                foundBusinesses = foundBusinesses
                            .Where(b => b.Name.Contains(avm.BusinessName))
                            .OrderBy(b => b.Name)
                            ;
            }

            if (avm.Category != null)
            {
                foundBusinesses = foundBusinesses
                            .Where(b => b.Category.Contains(avm.Category))
                            .OrderBy(b => b.Category)
                            ;
            }

            if (avm.City != null)
            {
                foundBusinesses = foundBusinesses
                            .Where(b => b.City.Contains(avm.City))
                            .OrderBy(b => b.Name)
                            ;
            }

            //Composite Search Results
            return View("SearchResults", foundBusinesses.Include(b => b.Name));
        }

        [HttpGet]
        public IActionResult SearchResults()
        {

            return View();
        }

        //  UPDATE  //

        [HttpGet]
        public IActionResult UpdateBusiness(long id)
        {
            Business business = new Business { Id = id };
            _db.Businesses.Update(business);
            _db.SaveChanges();

            return RedirectToAction("Search");
        }

        //DELETE //

        [HttpGet]
        public IActionResult DeleteBusiness(long id)
        {
            Business business = new Business { Id = id };
            _db.Businesses.Remove(business);
            _db.SaveChanges();

            return RedirectToAction("Search"); 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
