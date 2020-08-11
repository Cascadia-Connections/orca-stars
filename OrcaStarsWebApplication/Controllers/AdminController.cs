using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OrcaStarsWebApplication.Models;
using OrcaStarsWebApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace OrcaStarsWebApplication.Controllers
{

    public class AdminController : Controller
    {
        // DATABASE INJECTION //
        private BitDataContext _db;

        // CONSTRUCTOR //
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdminController(BitDataContext db, IWebHostEnvironment HostEnv)
        {
            _db = db;
            webHostEnvironment = HostEnv;
        }

        // GET //

        [HttpGet] //THIS IS THE INDEX PAGE
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] //THIS IS THE FORM
        [Authorize]
        public IActionResult Form()
        {
            ApplicationViewModel avm = new ApplicationViewModel();
            //set parameters in srvm to make delete notification appear, passing name to next view
            avm.DisplayNotification = "none";
            avm.Notification = "none";
            avm.BusinessName = null;
            return View(avm);
        }

        [HttpGet] //THIS IS THE UPDATE/EDIT FORM
        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var bus = _db.Businesses.Single(b => b.Id == id);
            var con = _db.Contacts.Single(c => c.Id == bus.ContactId);
            var hrs = _db.Hours.Single(h => h.ID == bus.Hours);
            var soc = _db.SocialMedias.Single(s => s.ID == bus.Social);
            ApplicationViewModel avm = new ApplicationViewModel
            {
                /* Get Business General Info */
                Id = id,
                BusinessName = bus.Name,
                Description = bus.Description,
                BusinessPhone = bus.PhoneNumber,
                AddressLine1 = bus.Address1,
                AddressLine2 = bus.Address2,
                City = bus.City,
                State = bus.State,
                Country = bus.Country,
                Zip = bus.ZipCode,
                Website = bus.Website,
                Category = bus.Category,
                BusinessLogoHolder = bus.Logo,
                StoreLogoHolder = bus.StoreFront,

                /* Get Business Contact data */
                FirstName = con.FirstName,
                LastName = con.LastName,
                Email = con.Email,
                PhoneNumber = con.PhoneNumber,

                /* Get Hour data */
                SunO = hrs.SunO,
                MonO = hrs.MonO,
                TuesO = hrs.TuesO,
                WedO = hrs.WedO,
                ThursO = hrs.ThursO,
                FriO = hrs.FriO,
                SatO = hrs.SatO,
                SunC = hrs.SunC,
                MonC = hrs.MonC,
                TuesC = hrs.TuesC,
                WedC = hrs.WedC,
                ThursC = hrs.ThursC,
                FriC = hrs.FriC,
                SatC = hrs.SatC,

                /* Get Social Media data */
                Facebook = soc.Facebook,
                Instagram = soc.Instagram,
                Twitter = soc.Twitter
            };
            return View(avm);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(Guid id, ApplicationViewModel avm)
        {
            var bus = _db.Businesses.Single(b => b.Id == id);
            var con = _db.Contacts.Single(c => c.Id == bus.ContactId);
            var hrs = _db.Hours.Single(h => h.ID == bus.Hours);
            var soc = _db.SocialMedias.Single(s => s.ID == bus.Social);
            if (ModelState.IsValid/* && avm.Category != "--Select--"*/)
            {
                string uniqueBusinessFileName = null;
                string uniqueStoreFileName = null;

                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/uploads"); //images location as string format

                if (avm.BusinessLogo != null)
                {
                    uniqueBusinessFileName = Guid.NewGuid().ToString() + "_" + avm.BusinessLogo.FileName.Substring(Math.Max(0, avm.BusinessLogo.FileName.Length - 8), Math.Min(avm.BusinessLogo.FileName.Length, 8)); //make sure uploaded file is unique, only keeping the last few letters from the file name so it doesn't break
                    string filePath = Path.Combine(uploadsFolder, uniqueBusinessFileName); //combining uploads folder and unique file name to create it files path
                    avm.BusinessLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                    avm.BusinessLogoHolder = "images/uploads/" + uniqueBusinessFileName;
                    bus.Logo = avm.BusinessLogoHolder;
                }

                if (avm.StoreLogo != null)
                {
                    uniqueStoreFileName = Guid.NewGuid().ToString() + "_" + avm.StoreLogo.FileName.Substring(Math.Max(0, avm.StoreLogo.FileName.Length - 8), Math.Min(avm.StoreLogo.FileName.Length, 8)); //make sure uploaded file is unique, only keeping the last few letters from the file name so it doesn't break.
                    string filePath = Path.Combine(uploadsFolder, uniqueStoreFileName); //combining uploads folder and unique file name to create it files path
                    avm.StoreLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                    avm.StoreLogoHolder = "images/uploads/" + uniqueStoreFileName;
                    bus.StoreFront = avm.StoreLogoHolder;
                }

                bus.Name = avm.BusinessName;
                bus.Description = avm.Description;
                bus.PhoneNumber = avm.BusinessPhone;
                bus.Address1 = avm.AddressLine1;
                bus.Address2 = avm.AddressLine2;
                bus.City = avm.City;
                bus.State = avm.State;
                bus.Country = avm.Country;
                bus.ZipCode = avm.Zip;
                bus.Website = avm.Website;
                bus.Category = avm.Category;
                bus.Hours = hrs.ID;
                bus.Social = soc.ID;
                bus.ContactId = con.Id;
                _db.Businesses.Update(bus);

                hrs.SunO = avm.SunO;
                hrs.SunC = avm.SunC;
                hrs.MonO = avm.MonO;
                hrs.MonC = avm.MonC;
                hrs.TuesO = avm.TuesO;
                hrs.TuesC = avm.TuesC;
                hrs.WedO = avm.WedO;
                hrs.WedC = avm.WedC;
                hrs.ThursO = avm.ThursO;
                hrs.ThursC = avm.ThursC;
                hrs.FriO = avm.FriO;
                hrs.FriC = avm.FriC;
                hrs.SatO = avm.SatO;
                hrs.SatC = avm.SatC;
                _db.Hours.Update(hrs);

                soc.Twitter = avm.Twitter;
                soc.Facebook = avm.Facebook;
                soc.Instagram = avm.Instagram;
                _db.SocialMedias.Update(soc);

                con.FirstName = avm.FirstName;
                con.LastName = avm.LastName;
                con.PhoneNumber = avm.PhoneNumber;
                con.Email = avm.Email;
                _db.Contacts.Update(con);

                _db.SaveChanges();
                return RedirectToAction("ConfirmDisplay", new { id = id });
            }
            return View();
        }

        // CREATE //
        [HttpPost] //THIS PUSHES FORM DATA TO DATA BASE
        [Authorize]
        public IActionResult Form (ApplicationViewModel avm)
        {
            avm.DisplayNotification = "none";
            avm.Duplicate = "none";
            /* Make sure the model is filled in properly */
            if (!ModelState.IsValid)
            {
                avm.DisplayNotification = "block";
                avm.Notification = "Some required fields are missing";
                return View(avm);
            }

            /* Check that the business is unique */
            Business foundBusinesses = _db.Businesses.FirstOrDefault(b => b.Name == avm.BusinessName);
            if (null != foundBusinesses)
            {
                avm.Duplicate = "block";
                avm.DisplayNotification = "block";
                avm.Notification = "The business " + avm.BusinessName + " already exists."; //Eventually check against address as well. 
                avm.ExistingId = foundBusinesses.Id;

                return View(avm);
            }

            return RedirectToAction("AddBusiness", avm);
        }

        // DELETE //

        [HttpGet]
        public IActionResult SaveNew(Guid id)
        {
            return RedirectToAction("ConfirmDisplay", id);
        }
        [HttpGet]
        public IActionResult EditExisting(Guid id)
        {
            Business business = _db.Businesses.Single(b => b.Id == id);

            Business foundBusinesses = _db.Businesses.FirstOrDefault(b => b.Name == business.Name);

            //set parameters in srvm to make delete notification appear, passing name to next view
            _db.Businesses.Remove(business);
            _db.SaveChanges();

            return RedirectToAction("Edit", foundBusinesses.Id);
        }


        [HttpGet]
        public IActionResult AddBusiness(ApplicationViewModel avm)
        {
            string uniqueBusinessFileName = null;
            string uniqueStoreFileName = null;

            avm.BusinessLogoHolder = "images/orcastarsImages/defaultBusinessStorelogo.png";
            avm.StoreLogoHolder = "images/orcastarsImages/defaultBusinessStorelogo.png";

            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/uploads"); //images location as string format

            if (avm.BusinessLogo != null)
            {
                uniqueBusinessFileName = Guid.NewGuid().ToString() + "_" + avm.BusinessLogo.FileName.Substring(Math.Max(0, avm.BusinessLogo.FileName.Length - 8), Math.Min(avm.BusinessLogo.FileName.Length, 8)); //make sure uploaded file is unique, only keeping the last few letters from the file name so it doesn't break
                string filePath = Path.Combine(uploadsFolder, uniqueBusinessFileName); //combining uploads folder and unique file name to create it files path
                avm.BusinessLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                avm.BusinessLogoHolder = "images/uploads/" + uniqueBusinessFileName;
            }

            if (avm.StoreLogo != null)
            {
                uniqueStoreFileName = Guid.NewGuid().ToString() + "_" + avm.StoreLogo.FileName.Substring(Math.Max(0, avm.StoreLogo.FileName.Length - 8), Math.Min(avm.StoreLogo.FileName.Length, 8)); //make sure uploaded file is unique, only keeping the last few letters from the file name so it doesn't break.
                string filePath = Path.Combine(uploadsFolder, uniqueStoreFileName); //combining uploads folder and unique file name to create it files path
                avm.StoreLogo.CopyTo(new FileStream(filePath, FileMode.Create)); //copy photo to server
                avm.StoreLogoHolder = "images/uploads/" + uniqueStoreFileName;
            }

            Hours hours = new Hours
            {
                SunO = avm.SunO,
                SunC = avm.SunC,
                MonO = avm.MonO,
                MonC = avm.MonC,
                TuesO = avm.TuesO,
                TuesC = avm.TuesC,
                WedO = avm.WedO,
                WedC = avm.WedC,
                ThursO = avm.ThursO,
                ThursC = avm.ThursC,
                FriO = avm.FriO,
                FriC = avm.FriC,
                SatO = avm.SatO,
                SatC = avm.SatC
            };
            _db.Hours.Add(hours);

            SocialMedia socialM = new SocialMedia
            {
                Twitter = avm.Twitter,
                Facebook = avm.Facebook,
                Instagram = avm.Instagram
            };
            _db.SocialMedias.Add(socialM);

            BusinessContact businessContact = new BusinessContact
            {
                FirstName = avm.FirstName,
                LastName = avm.LastName,
                PhoneNumber = avm.PhoneNumber,
                Email = avm.Email
            };

            _db.Contacts.Add(businessContact);

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
                Hours = hours.ID,
                Social = socialM.ID,
                Logo = avm.BusinessLogoHolder,
                StoreFront = avm.StoreLogoHolder,
                ContactId = businessContact.Id

            };

            _db.Businesses.Add(business);
            _db.SaveChanges();

            return RedirectToAction("ConfirmDisplay", new { id = business.Id });
        }
        [HttpGet] //DISPLAYS BUSINESS INFO
        [Authorize]
        public IActionResult ConfirmDisplay(Guid id)
        {
            var bus = _db.Businesses.Single(b => b.Id == id);
            var con = _db.Contacts.Single(c => c.Id == bus.ContactId);
            var hrs = _db.Hours.Single(h => h.ID == bus.Hours);
            var soc = _db.SocialMedias.Single(s => s.ID == bus.Social);
            ApplicationViewModel avm = new ApplicationViewModel
            {
                /* Get Business General Info */
                Id = id,
                BusinessName = bus.Name,
                Description = bus.Description,
                BusinessPhone = bus.PhoneNumber,
                AddressLine1 = bus.Address1,
                AddressLine2 = bus.Address2,
                City = bus.City,
                State = bus.State,
                Country = bus.Country,
                Zip = bus.ZipCode,
                Website = bus.Website,
                Category = bus.Category,
                BusinessLogoHolder = bus.Logo,
                StoreLogoHolder = bus.StoreFront,

                /* Get Business Contact data */
                FirstName = con.FirstName,
                LastName = con.LastName,
                Email = con.Email,
                PhoneNumber = con.PhoneNumber,

                /* Get Hour data */
                SunO = hrs.SunO,
                MonO = hrs.MonO,
                TuesO = hrs.TuesO,
                WedO = hrs.WedO,
                ThursO = hrs.ThursO,
                FriO = hrs.FriO,
                SatO = hrs.SatO,
                SunC = hrs.SunC,
                MonC = hrs.MonC,
                TuesC = hrs.TuesC,
                WedC = hrs.WedC,
                ThursC = hrs.ThursC,
                FriC = hrs.FriC,
                SatC = hrs.SatC,

                /* Get Social Media data */
                Facebook = soc.Facebook,
                Instagram = soc.Instagram,
                Twitter = soc.Twitter
            };
            return View(avm);
        }

        [HttpGet] //DISPLAYS BUSINESS INFO
        [Authorize]
        public IActionResult Confirm(ApplicationViewModel avm)
        {
            return View(avm);
        }

        // READ AND SEARCH //

        [HttpGet]
        [Authorize]
        public IActionResult Search()
        {
            //obtain businesses from database to pass to view
            IQueryable<Business> foundBusinesses = _db.Businesses.OrderBy(b => b.Id);

            SearchViewModel svm = new SearchViewModel();
            svm.businessNames = new List<string>();
            svm.businessCategories = new List<string>();
            svm.businessCities = new List<string>();

            //build a list of businesses with only the names, category and city and assign to searchviewmodel businesses
            foreach (Business business in foundBusinesses)
            {
                svm.businessNames.Add(business.Name);
                svm.businessCategories.Add(business.Category);
                svm.businessCities.Add(business.City);
            }
            return View(svm);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Search(ApplicationViewModel avm)
        {
            //Full Collection Search - start with entire collection
            IQueryable<Business> foundBusinesses = _db.Businesses.OrderBy(b => b.Id);

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
                            .OrderBy(b => b.Name)
                            ;
            }

            if (avm.City != null)
            {
                foundBusinesses = foundBusinesses
                            .Where(b => b.City.Contains(avm.City))
                            .OrderBy(b => b.Name)
                            ;
            }

            //create searchResultsVM
            SearchResultsViewModel srvm = new SearchResultsViewModel()
            {
                displayDeleteNotification = "none",
                deletedBusinessName = "",
                businesses = foundBusinesses
            };

            //Composite Search Results
            return View("SearchResults", srvm);
        }

        // UPDATE //

        [HttpGet]
        [Authorize]
        public IActionResult UpdateBusiness(Guid id)
        {
            Business business = new Business { Id = id };
            _db.Businesses.Update(business);
            _db.SaveChanges();

            return View("Search");
        }

        // DELETE //
        [HttpGet]
        public IActionResult ConfirmDelete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                Business business = _db.Businesses.Single(b => b.Id == id);
                ConfirmDeleteViewModel confirmDeleteVM = new ConfirmDeleteViewModel();
                confirmDeleteVM.Id = id;
                confirmDeleteVM.Business = business;
                return View("ConfirmDelete", confirmDeleteVM);
            }
        }

        [HttpPost]
        public IActionResult ConfirmDelete(ConfirmDeleteViewModel cdvm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Search");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteBusiness(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Business business = _db.Businesses.Single(b => b.Id == id);

            //create searchResultsVM
            SearchResultsViewModel srvm = new SearchResultsViewModel();
            //set parameters in srvm to make delete notification appear, passing name to next view
            srvm.displayDeleteNotification = "block";
            srvm.deletedBusinessName = business.Name;
            _db.Businesses.Remove(business);

            _db.SaveChanges();
            return RedirectToAction("DeleteSuccessful", srvm); 
        }

        [Authorize]
        public IActionResult DeleteSuccessful(SearchResultsViewModel vm)
        {
            SearchResultsViewModel srvm = vm;
            IQueryable<Business> foundBusinesses = _db.Businesses.OrderBy(b => b.Id);
            srvm.businesses = foundBusinesses;
            return View("SearchResults", srvm);
        }
    }
}
