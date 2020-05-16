using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaStarsWebApplication.Models;

namespace OrcaStarsWebApplication.Repositories
{
    public interface IBusinessRepository
    {
        public void SetBusiness(Business b);
        public IEnumerable<Business> GetBusiness();
        public int GetCount();
    }
}
