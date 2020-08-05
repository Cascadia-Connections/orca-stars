using OrcaStarsWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.ViewModels
{
    public class ConfirmDeleteViewModel
    {
        public Guid Id { get; set; }
        public Business Business { get; set; }
    }
}
