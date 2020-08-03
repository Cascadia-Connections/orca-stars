using Microsoft.AspNetCore.Hosting;
using OrcaStarsWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Utilities
{
    public class ValidNewBusinessAttribute : ValidationAttribute
    {
        // DATABASE INJECTION //
        private BitDataContext _db;

        // CONSTRUCTOR //
        private readonly IWebHostEnvironment webHostEnvironment;
        public ValidNewBusinessAttribute(BitDataContext db, IWebHostEnvironment HostEnv)
        {
            _db = db;
            webHostEnvironment = HostEnv;
        }
        public override bool IsValid(object value)
        {
            /* Business exists: model.values.somethingorother - INVALID: FALSE */
            /* Business doesn't exist: null - VALID: TRUE */
            Business foundBusinesses = _db.Businesses.FirstOrDefault(b => b.Name == value.ToString());
            return null == foundBusinesses;
        }

    }
}
