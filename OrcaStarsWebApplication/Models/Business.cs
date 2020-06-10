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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Logo { get; set; }
        public string StoreFront { get; set; } 
        public Guid ContactId { get; set; }
        public Guid Social { get; set; }
        public Guid Hours { get; set; }
    }
}
