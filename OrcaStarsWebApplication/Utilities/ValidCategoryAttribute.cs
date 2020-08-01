using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrcaStarsWebApplication.Utilities
{
    public class ValidCategoryAttribute : ValidationAttribute
    {
        private readonly string InvalidCategory;

        public ValidCategoryAttribute(string InvalidCategory)
        {
            this.InvalidCategory = InvalidCategory;
        }
        public override bool IsValid(object value)
        {
            string val = value.ToString();
            return val.ToUpper() != InvalidCategory.ToUpper();
        }
    }
}
