using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class ApplicationViewModel
    {
        [Display(Name = "Username (Email)")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

    }
}
