using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaStarsWebApplication.ViewModels;

namespace OrcaStarsWebApplication.Models
{
    public class AddressService
    {
        public string fieldsToAddress(ApplicationViewModel avm)
        {
            var address = "";
            if (avm.AddressLine1 != null)
                address += avm.AddressLine1 + ", ";
            if (avm.AddressLine2 != null)
                address += avm.AddressLine2 + " ";
            if (avm.City != null)
                address += avm.City + " ";
            if (avm.State != null)
                address += avm.State;
            if (avm.Zip != 0) //changed from Null to 0 for int data type
                address += " " + avm.Zip;
            return address;
        }
    }
}
