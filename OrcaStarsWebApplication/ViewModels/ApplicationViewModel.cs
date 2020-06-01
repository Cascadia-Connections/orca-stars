using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

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
        [Required(ErrorMessage = "*Required")]
        public string Category { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string State { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Country { get; set; }
        public string Zip { get; set; }
        public string SocialMedia { get; set; }
        public string SocialMedia2 { get; set; }
        public string Handle { get; set; }
        public string Handle2 { get; set; }
        public string Hours { get; set; }
        public IFormFile BusinessLogo { get; set; } //changed to IFormFile as file uploaded to service can be accessed by database by this data type
        public IFormFile StoreLogo { get; set; } //changed to IFormFile as file uploaded to service can be accessed by database by this data type
        public string BusinessLogoHolder { get;  set; }
        public string StoreLogoHolder { get;  set; }

    }
}
