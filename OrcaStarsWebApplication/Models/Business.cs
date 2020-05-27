using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;


namespace OrcaStarsWebApplication.Models
{
    public class Business
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PhoneNumber { get; set; }
        public Point Coordinates { get; set; } //change to geolocation not system.draw  
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; } //if they have particular needs for the api and what theyre needs are 
        public int ZipCode { get; set; }
        public SocialMedia Social { get; set; } //collection or class
        public List<Hours> Hours { get; set; } //unsure of data collection type to use
        public Bitmap Logo { get; set; } 
        public Bitmap StoreFront { get; set; }
    }
}
