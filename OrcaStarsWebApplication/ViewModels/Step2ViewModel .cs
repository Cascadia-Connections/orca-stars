using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class Step2ViewModel
    {
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string Description { get; set; }
        public string Website { get; set; }
        public string SocialMedia { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Logo { get; set; }
        public string Store { get; set; }

    }
}
