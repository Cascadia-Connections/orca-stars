﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class ApplicationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string Description { get; set; }
        public string Website { get; set; }
        public string Coordinates { get; set; }
        public string SocialMedia { get; set; }
        public string Hours { get; set; }
        public string Logo { get; set; }
    }
}
