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
            string mon = hrs.MonO + "-" + hrs.MonC;
            string tues = hrs.TuesO + "-" + hrs.TuesC;
            string wed = hrs.WedO + "-" + hrs.WedC;
            string thurs = hrs.ThursO + "-" + hrs.ThursC;
            string fri = hrs.FriO + "-" + hrs.FriC;
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
            /* See if all the weekday times are the same */
            if (hrs.MonO == hrs.TuesO && hrs.MonO == hrs.WedO && hrs.MonO == hrs.ThursO && hrs.MonO == hrs.FriO &&
                hrs.MonC == hrs.TuesC && hrs.MonC == hrs.WedC && hrs.MonC == hrs.ThursC && hrs.MonC == hrs.FriC)
            {
                /* See if Saturday Matches */
                if (hrs.MonO == hrs.SatO && hrs.MonC == hrs.SatC)
                {
                    /* See if Sunday Matches */
                    if (hrs.MonO == hrs.SunO && hrs.MonC == hrs.SunC)
                    {
                        /* ENDING: Same Hours Daily */
                        if (hrs.MonO == "Closed" || hrs.SunC == "Closed")
                        {
                            return "Closed";
                        }
                        return hrs.MonO + "-" + hrs.SunC + " Daily";
                    }
                    /* ENDING: Monday-Saturday: Open-Close, Sunday: Open-Close */
                    if (hrs.MonO == "Closed" || hrs.SatC == "Closed")
                    {
                        return "Closed Monday-Saturday, Sunday: " + hrs.SunO + "-" + hrs.SunC;
                    }
                    return "Monday-Saturday: " + hrs.MonO + "-" + hrs.SatC + ", Sunday: " + hrs.SunO + "-" + hrs.SunC;
                }
                /* ENDING: Monday-Friday: Open-Close, Saturday: Open-Close, Sunday: Open-Close */
                if (hrs.MonO == "Closed" || hrs.FriC == "Closed")
                {
                    string sat = hrs.SatO + "-" + hrs.SatC;
                    string sun = hrs.SunO + "-" + hrs.SunC;
                    if (hrs.SatO == "Closed" || hrs.SatC == "Closed")
                        sat = "Closed";
                    if (hrs.SunO == "Closed" || hrs.SunC == "Closed")
                        sun = "Closed";
                    return "Monday-Friday: Closed, Saturday: " + sat + ", Sunday: " + sun;
                }
                hours += "Monday-Friday: " + hrs.MonO + "-" + hrs.FriC;
            }
            else
            {
                /* Monday: Open-Close, Tuesday: Open-Close, Wednesday: Open-Close, Thursday: Open-Close, Friday: Open-Close, */
                hours += "Monday: " + hrs.MonO + "-" + hrs.MonC + ", Tuesday: " + hrs.TuesO + "-" + hrs.TuesC + ", Wednesday: " + hrs.WedO + "-" + hrs.WedC + ", Thursday: " + hrs.ThursO + "-" + hrs.ThursC + ", Friday: " + hrs.FriO + "-" + hrs.FriC;
            }
            /* See if the weekend times are the same */
            if (hrs.SatO == hrs.SunO && hrs.SatC == hrs.SunC)
            {
                /* ENDING: hours+ " Weekends: Open-Close" */
                return hours + ", Weekends: " + hrs.SatO + "-" + hrs.SunC;
            }
            /* ENDING: hours + " Saturday: Open-Close, Sunday: Open-Close"*/
            return hours + ", Saturday: " + hrs.SatO + "-" + hrs.SatC + ", Sunday: " + hrs.SunO + "-" + hrs.SunC;
        }
    }
}
