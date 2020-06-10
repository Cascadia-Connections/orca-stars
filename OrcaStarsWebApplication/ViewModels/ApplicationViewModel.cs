using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using OrcaStarsWebApplication.Models;

namespace OrcaStarsWebApplication.ViewModels
{
    public class ApplicationViewModel
    {
        public Guid Id { get; set; }

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
        public string BusinessPhone { get; set; }
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
        public SocialMedia Social { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public Hours Hours { get; set; }
        public string SunO { get; set; }
        public string SunC { get; set; }
        public string MonO { get; set; }
        public string MonC { get; set; }
        public string TuesO { get; set; }
        public string TuesC { get; set; }
        public string WedO { get; set; }
        public string WedC { get; set; }
        public string ThursO { get; set; }
        public string ThursC { get; set; }
        public string FriO { get; set; }
        public string FriC { get; set; }
        public string SatO { get; set; }
        public string SatC { get; set; }
        public IFormFile BusinessLogo { get; set; } //changed to IFormFile as file uploaded to service can be accessed by database by this data type
        public IFormFile StoreLogo { get; set; } //changed to IFormFile as file uploaded to service can be accessed by database by this data type
        public string BusinessLogoHolder { get;  set; }
        public string StoreLogoHolder { get;  set; }

    }
}
