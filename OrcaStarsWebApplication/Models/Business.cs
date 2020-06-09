using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace OrcaStarsWebApplication.Models
{
    public class Business
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public SocialMedia Social { get; set; } 
        public ICollection<Hours> Hours { get; set; } 
        public IFormFile Logo { get; set; } //changed from bitmap to iformfile
        public IFormFile StoreFront { get; set; } //changed from bitmap to iformfile
    }
}
