using OrcaStarsWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class SearchResultsViewModel
    {
        //initialise businesses in constructor
        public SearchResultsViewModel() 
        {
            //initialise business as list of type Business
            businesses = new List<Business>();
        }
        public string displayDeleteNotification { get; set; }
        public string deletedBusinessName { get; set; }
        
        //Now, view will be able to use the get to pull list of businesses
        public IEnumerable<Business> businesses{ get; set; }

        //Other New Stuff
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
