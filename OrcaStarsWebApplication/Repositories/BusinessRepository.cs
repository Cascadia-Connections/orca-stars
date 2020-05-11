using OrcaStarsWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        List<Business> businesses = new List<Business>();
        Business business = new Business();
        public Business GetBusiness()
        {
            return business;
        }

        public int GetCount()
        {
            return 1;
        }

        public void SetBusiness(Business b)
        {
            business = b;
        }

        IEnumerable<Business> IBusinessRepository.GetBusiness()
        {
            return businesses;
        }
    }
}
