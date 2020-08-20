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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public IActionResult Index(Guid id)
        {
            Guid newid = new Guid();
            IndexViewModel ivm = new IndexViewModel();
            if (newid == id || null == id)
            {
                ivm.Display = "none";
                ivm.BusinessName = "";
                return View(ivm);
            }
            ivm.BusinessName = _db.Businesses.Single(b => b.Id == id).Name;
            ivm.Display = "block";
            return View(ivm);

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
            PhoneService ps = new PhoneService();

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
                bus.PhoneNumber = ps.formatNumber(avm.BusinessPhone);
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
                con.PhoneNumber = ps.formatNumber(avm.PhoneNumber);
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
        public IActionResult Form(ApplicationViewModel avm)
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
            /* First grab only business that have the same name */
            IQueryable<Business> foundBusinesses = _db.Businesses.Where(b => b.Name == avm.BusinessName);

            /* Compare the business' address */
            if (foundBusinesses.Count() > 0)
            {
                foundBusinesses = foundBusinesses.Where(b => b.Address1 == avm.AddressLine1);
            }
            if (foundBusinesses.Count() > 0)
            {
                foundBusinesses = foundBusinesses.Where(b => b.Address2 == avm.AddressLine2);
            }
            if (foundBusinesses.Count() > 0)
            {
                foundBusinesses = foundBusinesses.Where(b => b.ZipCode == avm.Zip);
            }
            if (foundBusinesses.Count() > 0)
            {
                foundBusinesses = foundBusinesses.Where(b => b.City == avm.City);
            }
            if (foundBusinesses.Count() > 0)
            {
                foundBusinesses = foundBusinesses.Where(b => b.State == avm.State);
            }
            if (foundBusinesses.Count() > 0)
            {
                foundBusinesses = foundBusinesses.Where(b => b.Country == avm.Country);
            }
            /* if the business matches at least one existing business still, it's a duplicate. */
            if (foundBusinesses.Count() > 0)
            {
                /* show the error */
                avm.Duplicate = "block";
                avm.DisplayNotification = "block";
                avm.Notification = "The business " + avm.BusinessName + " already exists.";
                Business foundbus = foundBusinesses.FirstOrDefault(b => b.Name == avm.BusinessName);
                avm.ExistingId = foundbus.Id;
                return View(avm);
            }

            string uniqueBusinessFileName = null;
            string uniqueStoreFileName = null;
            PhoneService ps = new PhoneService();

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
                PhoneNumber = ps.formatNumber(avm.PhoneNumber),
                Email = avm.Email
            };

            _db.Contacts.Add(businessContact);

            Business business = new Business
            {

                Name = avm.BusinessName,
                Description = avm.Description,
                PhoneNumber = ps.formatNumber(avm.BusinessPhone),
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

        // READ AND SEARCH //

        [HttpGet]
        [Authorize]
        public IActionResult Search()
        {
            //obtain businesses from database to pass to view
            IQueryable<Business> foundBusinesses = _db.Businesses.OrderBy(b => b.Id);

            SearchViewModel svm = new SearchViewModel();
            svm.businessNames = new List<string>();
            svm.businessCities = new List<string>();

            //build a list of businesses with only the names and city and assign to searchviewmodel businesses
            //the foreach loop will add business names and cities to each respective list
            //it will also look out for duplicates and make sure they are not added to the list
            foreach (Business business in foundBusinesses)
            {
                bool addName = true;
                bool addCity = true;
                if (svm.businessNames.Count() > 0)
                {
                    if (svm.businessNames.Contains(business.Name))
                        addName = false;
                }
                if (svm.businessCities.Count() > 0)
                {
                    if (svm.businessCities.Contains(business.City))
                        addCity = false;
                }
                if(addName)
                    svm.businessNames.Add(business.Name);
                if (addCity)
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

            if (avm.Category != "ALL")
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

        [HttpGet]
        public IActionResult SearchResults(SearchResultsViewModel srvm)
        {
            IQueryable<Business> foundBusinesses = _db.Businesses.OrderBy(b => b.Id);
            srvm.displayDeleteNotification = "none";
            srvm.deletedBusinessName = "";
            srvm.businesses = foundBusinesses;
            return View(srvm);
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

            //If business Logo != "images/orcastarsImages/defaultBusinessStorelogo.png"; - Default
            //find logo in our uploads folder via its id
            //delete it

            if (business.Logo != "images/orcastarsImages/defaultBusinessStorelogo.png")
            {
                string bLogo = Path.Combine(webHostEnvironment.WebRootPath, business.Logo);   
                if (System.IO.File.Exists(bLogo))
                {
                    System.IO.File.Delete(bLogo);
                }
            }
            //If business storefront != "images/orcastarsImages/defaultBusinessStorelogo.png"; - Default
            //find storefront in our uploads folder via its id
            //delete it
            if (business.StoreFront != "images/orcastarsImages/defaultBusinessStorelogo.png")
            {
                string bStoreFront= Path.Combine(webHostEnvironment.WebRootPath, business.StoreFront);
                if (System.IO.File.Exists(bStoreFront))
                {
                    System.IO.File.Delete(bStoreFront);
                }
            }
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
