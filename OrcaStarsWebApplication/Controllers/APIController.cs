using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrcaStarsWebApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace OrcaStarsWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private BitDataContext _dbc;
        public APIController(BitDataContext dbc)
        {
            _dbc = dbc;
        }
        [HttpGet("{Name}")]
        public IActionResult Get(string term)//This is how we will be querying to find the correct business to return it's information as a Json object.  I was thinking we can test with name then switch to business ID when we know its working
        {
            var businesses = _dbc.Businesses.Where(b => b.Name.Contains(term))
                .Select(b => new 
                {
                     id = b.Id, 
                     name = b.Name, 
                     description = b.Description,
                     coordinates = b.Coordinates,
                     hours = b.Hours, 
                     logo = b.Logo,                     
                });
            return Json(businesses);
        }

        private IActionResult Json(IQueryable<object> businesses)
        {
            throw new NotImplementedException();
        }
    }
}