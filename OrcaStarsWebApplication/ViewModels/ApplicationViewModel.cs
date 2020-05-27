using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
        public string Email { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string Description { get; set; }
        public string Website { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string SocialMedia { get; set; }
        public string Hours { get; set; }
        public IFormFile Logo { get; set; } //changed to IFormFile as file uploaded to service can be accessed by database by this data type
        public string BusinessLogo { get;  set; }
    }
}
