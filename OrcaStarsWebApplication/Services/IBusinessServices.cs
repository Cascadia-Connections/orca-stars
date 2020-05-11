    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Services
{
    interface IBusinessServices
    {
        public bool CheckNameNotNullOrEmpty();

        public bool CheckDescriptionNotNullOrEmpty();
    }
}
