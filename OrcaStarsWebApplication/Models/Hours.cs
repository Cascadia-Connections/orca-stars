using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Models
{
    public class Hours
    {
        public Guid ID { get; set; }
        public string Day { get; set; }
        public string OpenHour { get; set; }
        public string OpenMinute { get; set; }
        public string CloseHour { get; set; }
        public string CloseMinute { get; set; }
    }
}
