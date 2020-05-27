﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class ApplicationViewModel
    {
        [Required(ErrorMessage = "*Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Description { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string SocialMedia { get; set; }
        public string Hours { get; set; }
        public string Logo { get; set; }
    }
}
