using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Email { get; set; }
    }
}
