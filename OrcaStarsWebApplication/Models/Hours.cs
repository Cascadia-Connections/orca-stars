using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcaStarsWebApplication.Models
{
    public class Hours
    {
        public DayOfWeek Day { get; set; }
        public string OpenHour { get; set; }
        public string OpenMinute { get; set; }
        public string CloseHour { get; set; }
        public string CloseMinute { get; set; }
        public List<Hours> ListHours { get; set; }

        //CONSTRUCTOR//
        public Hours(DayOfWeek theDay, string startHour, string startMinute, string endHour, string endMinute)
        {
            Day = theDay;
            OpenHour = startHour ?? "00";
            OpenMinute = startMinute ?? "00";
            CloseHour = endHour ?? "00";
            CloseMinute = endMinute ?? "00";
        }

        //STRING FORMAT METHOD//
        public string HoursOfOperation()
        {
            return String.Format($"{Day} : {OpenHour}:{OpenMinute} AM - {CloseHour}:{CloseMinute} PM");
        }
    }
}
