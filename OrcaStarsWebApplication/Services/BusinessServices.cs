using OrcaStarsWebApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaStarsWebApplication.Models;
using OrcaStarsWebApplication.Services;

namespace OrcaStarsWebApplication.Services
{
    public class BusinessServices : IBusinessServices
    {
        private IBusinessRepository iBusRepo;

        public BusinessServices(IBusinessRepository ibr)
        {
            iBusRepo = ibr;
        }

        public bool CheckDescriptionNotNullOrEmpty()
        {
            IEnumerable<Business> b = iBusRepo.GetBusiness();
            var bus = b.FirstOrDefault(b => b.Description == "");

            bool result = string.IsNullOrEmpty(bus.Description) ? true : false;
            return result;
        }

        public bool CheckNameNotNullOrEmpty()
        {
            IEnumerable<Business> b = iBusRepo.GetBusiness();
            var bus = b.FirstOrDefault(b => b.Name == "");
            
            bool result = string.IsNullOrEmpty(bus.Name) ? true : false;
            return result;
        }
    }
}
