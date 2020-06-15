using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrcaStarsWebApplication.ViewModels;

namespace OrcaStarsWebApplication.Models
{
    public class HoursService
    {
        public string hoursToString(ApplicationViewModel hrs)
        {
            var hours = "";
            /* combine the hours */
            string mon = hrs.MonO + "-" + hrs.MonC;
            string tues = hrs.TuesO + "-" + hrs.TuesC;
            string wed = hrs.WedO + "-" + hrs.WedC;
            string thurs = hrs.ThursO + "-" + hrs.ThursC;
            string fri = hrs.FriO + "-" + hrs.FriC;
            string sat = hrs.SatO + "-" + hrs.SatC;
            string sun = hrs.SunO + "-" + hrs.SunC;
            /* Overwrite Closed days with "Closed" */
            if (hrs.MonO == "Closed" || hrs.MonC == "Closed")
                mon = "Closed";
            if (hrs.TuesO == "Closed" || hrs.TuesC == "Closed")
                tues = "Closed";
            if (hrs.WedO == "Closed" || hrs.WedC == "Closed")
                wed = "Closed";
            if (hrs.ThursO == "Closed" || hrs.ThursC == "Closed")
                thurs = "Closed";
            if (hrs.FriO == "Closed" || hrs.FriC == "Closed")
                fri = "Closed";
            if (hrs.SatO == "Closed" || hrs.SatC == "Closed")
                sat = "Closed";
            if (hrs.SunO == "Closed" || hrs.SunC == "Closed")
                sun = "Closed";

            /* See if all the weekday times are the same */
            if (mon == tues && mon == wed && mon == thurs && mon == fri)
            {
                /* See if Saturday Matches */
                if (mon == sat)
                {
                    /* See if Sunday Matches */
                    if (mon == sun)
                    {
                        /* ENDING: Same Hours Every Day */
                        return mon + " Every Day";
                    }
                    /* ENDING: Monday-Saturday: Open-Close, Sunday: Open-Close */
                    return "Monday-Saturday: " + mon + ", Sunday: " + sun;
                }
                /* ENDING: Monday-Friday: Open-Close, Saturday: Open-Close, Sunday: Open-Close */
                hours += "Monday-Friday: " + mon;
            }
            else
            {
                /* Monday: Open-Close, Tuesday: Open-Close, Wednesday: Open-Close, Thursday: Open-Close, Friday: Open-Close, */
                hours += "Monday: " + mon + ", Tuesday: " + tues + ", Wednesday: " + wed + ", Thursday: " + thurs + ", Friday: " + fri;
            }
            /* See if the weekend times are the same */
            if (sat == sun)
            {
                /* ENDING: hours+ " Weekends: Open-Close" */
                return hours + ", Weekends: " + sat;
            }
            /* ENDING: hours + " Saturday: Open-Close, Sunday: Open-Close"*/
            return hours + ", Saturday: " + sat + ", Sunday: " + sun;
        }
    }
}
