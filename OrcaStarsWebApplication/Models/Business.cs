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
        public Point Coordinates { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        
        public SocialMedia Social { get; set; } //add class, reference here
        //public Hours Hours { get; set; }
        public string Hours { get; set; }
        public Bitmap Logo { get; set; } 
        public Bitmap Store { get; set; }

        //social media object - s.m data type as collection of s.m objects --TO DO
        //research standard s.m data type --TO DO
        //string address (add1, add2, city, state, zip) -- research standard --DONE
        //storefront image of business (extra bitmap DT) --DONE
        //business phone number?
    }
}
