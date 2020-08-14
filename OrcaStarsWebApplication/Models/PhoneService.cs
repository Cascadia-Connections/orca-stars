using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaStarsWebApplication.ViewModels;

namespace OrcaStarsWebApplication.Models
{
    public class PhoneService
    {
        public string formatNumber(string num)
        {
            var newNum = "(";
            newNum += num.Replace("(","").Replace(")","").Replace(" ", "").Replace("-", "");
            newNum = newNum.Insert(4, ") ");
            newNum = newNum.Insert(9, "-");
            return newNum;
        }

    }
}
