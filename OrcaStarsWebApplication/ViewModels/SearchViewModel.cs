using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaStarsWebApplication.Models;

namespace OrcaStarsWebApplication.ViewModels
{
    public class SearchViewModel
    {
        //BusinessName
        public string BusinessName { get; set; }
        //Category
        public string Category { get; set; }
        //City
        public string City { get; set; }

        //enables passing business data from database to be used in search.cs view
        public List<string> businessNames { get; set; }
        public List<string> businessCities { get; set; }
    }
}
