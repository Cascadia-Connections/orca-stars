using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrcaStarsWebApplication.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcaStarsWebApplication.Controllers
{
    [Route("api/[controller]")]
    public class BusinessController : Controller
    {
        // GET: api/<controller>       
        private BitDataContext btc;
        public BusinessController(BitDataContext btc)
        {
            this.btc = btc;          
        }        
        
        // GET api/<controller>/5
        [Route("search/{category?}/{name?}")]
        public ActionResult Get(string category, string name)
        {
            IQueryable<Business> businesses = btc.Businesses.Include(b => b.Hours).Include(b =>b.Social);
            if (category == "null" && name == "null")
            {
                throw new Exception("No valid search data");
            }
            if(category == "null")
            {
                var query = from a in btc.Businesses 
                            join b in btc.Hours on a.Hours equals b.ID
                            join c in btc.SocialMedias on a.Social equals c.ID
                            where a.Name.Contains(name)
                            select new
                { name = a.Name, 
                  description = a.Description,
                  phonenumber = a.PhoneNumber,
                  category = a.Category,
                  website = a.Website,
                  address1 = a.Address1,
                  address2 = a.Address2,
                  city = a.City,
                  state = a.State,
                  country = a.Country,
                  zipcode = a.ZipCode,
                  logo = a.Logo,
                  storefront = a.StoreFront,
                  suno = b.SunO,
                  sunc = b.SunC,
                  mono = b.MonO,
                  monc = b.MonC,
                  tueso = b.TuesO,
                  tuesc = b.TuesC,
                  wedo = b.WedO,
                  wedc = b.WedC,
                  thurso = b.ThursO,
                  thursc = b.ThursC,
                  frio = b.FriO,
                  fric = b.FriC,
                  sato = b.SatO,
                  satc = b.SatC,
                  facebook = c.Facebook,
                  twitter = c.Twitter,
                  instagram = c.Instagram
                };                                    
                return Json(query);
                
            }
            else if(name == "null")
            {
                var query = from a in btc.Businesses
                            join b in btc.Hours on a.Hours equals b.ID
                            join c in btc.SocialMedias on a.Social equals c.ID
                            where a.Category.Equals(category)
                            select new
                  {
                  name = a.Name, 
                  description = a.Description,
                  phonenumber = a.PhoneNumber,
                  category = a.Category,
                  website = a.Website,
                  address1 = a.Address1,
                  address2 = a.Address2,
                  city = a.City,
                  state = a.State,
                  country = a.Country,
                  zipcode = a.ZipCode,
                  logo = a.Logo,
                  storefront = a.StoreFront,
                  suno = b.SunO,
                  sunc = b.SunC,
                  mono = b.MonO,
                  monc = b.MonC,
                  tueso = b.TuesO,
                  tuesc = b.TuesC,
                  wedo = b.WedO,
                  wedc = b.WedC,
                  thurso = b.ThursO,
                  thursc = b.ThursC,
                  frio = b.FriO,
                  fric = b.FriC,
                  sato = b.SatO,
                  satc = b.SatC,
                  facebook = c.Facebook,
                  twitter = c.Twitter,
                  instagram = c.Instagram
                };
                return Json(query);                
            }

            else
            {
                var query = from a in btc.Businesses
                            join b in btc.Hours on a.Hours equals b.ID
                            join c in btc.SocialMedias on a.Social equals c.ID
                            where a.Name.Contains(name) && a.Category.Equals(category)
                            select new
                 {
                  name = a.Name, 
                  description = a.Description,
                  phonenumber = a.PhoneNumber,
                  category = a.Category,
                  website = a.Website,
                  address1 = a.Address1,
                  address2 = a.Address2,
                  city = a.City,
                  state = a.State,
                  country = a.Country,
                  zipcode = a.ZipCode,
                  logo = a.Logo,
                  storefront = a.StoreFront,
                  suno = b.SunO,
                  sunc = b.SunC,
                  mono = b.MonO,
                  monc = b.MonC,
                  tueso = b.TuesO,
                  tuesc = b.TuesC,
                  wedo = b.WedO,
                  wedc = b.WedC,
                  thurso = b.ThursO,
                  thursc = b.ThursC,
                  frio = b.FriO,
                  fric = b.FriC,
                  sato = b.SatO,
                  satc = b.SatC,
                  facebook = c.Facebook,
                  twitter = c.Twitter,
                  instagram = c.Instagram
                };
                if (query != null)
                {
                    return Json(query);
                }
                throw new Exception("No data was found");
            }
            
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
