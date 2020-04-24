using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Models
{
    public class Business
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LongCoordinates { get; set; }
        public double LatCoordinates { get; set; }
        public string Hours { get; set; }
        public int Logo { get; set; } //Unsure how to store images to DB 
    }
}
