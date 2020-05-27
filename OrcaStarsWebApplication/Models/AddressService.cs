using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Models
{
    public class AddressService
    {
        public string fieldsToAddress(string AddrL1, string AddrL2, string City, string State, string Country)
        {
            var address = "";
            if (AddrL1 != null)
                address += AddrL1;
            if (AddrL2 != null)
                address += AddrL2;
            if (City != null)
                address += City;
            if (State != null)
                address += State;
            if (Country != null)
                address += Country;
            return address;
        }
    }
}
