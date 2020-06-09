using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Models
{
    public class Hours
    {
        public string Day { get; set; }
        public string OpenHour { get; set; }
        public string OpenMinute { get; set; }
        public string CloseHour { get; set; }
        public string CloseMinute { get; set; }
        public List<Hours> ListHours { get; set; }

        //CONSTRUCTOR//

        //STRING FORMAT METHOD//
        public string HoursOfOperation()
        {
            return String.Format($"{Day} : {OpenHour}:{OpenMinute} AM - {CloseHour}:{CloseMinute} PM");
        }
    }
}
