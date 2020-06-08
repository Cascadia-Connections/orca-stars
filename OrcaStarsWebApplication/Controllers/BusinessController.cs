using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
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
        private List<Business> btcTest;
        public BusinessController()
        {
            btcTest = new List<Business>//for the purpose of testing only 
            {
                new Business
                {
                    Name = "whole foods",
                    Description = "Organic foods grocery store",
                    PhoneNumber = 1234567890,
                    Category = "grocery",
                    City = "Bothell",
                    State = "WA"
                },
                new Business
                {
                    Name = "amazon fresh",
                    Description = "Online retailer for food stuffs",
                    PhoneNumber = 1231231231,
                    Category = "grocery"
                },
                new Business
                {
                    Name = "amazon prime",
                    Description = "Online video streaming",
                    PhoneNumber = 1231231231,
                    Category = "online"
                },
                new Business
                {
                    Name = "ebay",
                    Description = "Online retailer",
                    PhoneNumber = 1231231231,
                    Category = "online"
                }
            };
        }

        // GET api/<controller>/5
        [Route("search/{category?}/{name?}")]
        public ActionResult Get(string category, string name)
        {
            if(category == "null" && name == "null")
            {
                throw new Exception("No valid search data");
            }
            if(category == "null")
            {
                var query = from business in btcTest
                            where business.Name.Contains(name)
                            select business;
                return Json(query);
                
            }
            else if(name == "null")
            {
                var query = from business in btcTest
                            where business.Category.Contains(category)
                            select business;
                return Json(query);
            }

            else
            {
                var query = from business in btcTest
                            where business.Name.Contains(name)
                            where business.Category.Contains(category)
                            select business;
                if(query != null)
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
