using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Microsoft.AspNetCore.Http;
using OrcaStarsWebApplication.Models;

namespace XUnitTestOrcaStars.TestData
{
    public class BusinessData
    {
        private static List<Business> businesses = new List<Business>() 
        {
            new Business {Name = "", Description = "Lorem Ipsum"},
            new Business {Name = "John", Description = "Totem Pole"},
            new Business {Name = "", Description = "Mega Super Ultra"},
            new Business {Name = "Smith", Description = ""},

        };
        public static IEnumerable<Business> GetBusinesses()
        {
            return businesses;
        }

        public static int GetCount() 
        {
            return businesses.ToArray().Length;
        }
    }
}
