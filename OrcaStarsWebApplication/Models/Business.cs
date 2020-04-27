using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace OrcaStarsWebApplication.Models
{
    public class Business
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Point Coordinates { get; set; }
        public string Hours { get; set; }
        public string Logo { get; set; } //File path reference
    }
}
