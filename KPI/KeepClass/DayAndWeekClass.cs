using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.KeepClass
{
    public class DayAndWeekClass
    {
        public DayAndWeekClass()
        {

        }

        public PpasDisplayMember FindStartingDateInWeek(DateTime dt)
        //private (DateTime, int, int) FindStartingDateInWeek(DateTime dt)
        {
            PpasDisplayMember data = new PpasDisplayMember();
            //var data = (Startdate: DateTime.Now, DayInWeek: 0, NumberOfDate: 0);
            int daysOfweek = (int)dt.DayOfWeek;
            int day = dt.Day;
            DateTime dtstart = DateTime.Now; // default
            // Find starting date
            if (day >= daysOfweek && daysOfweek != 0)
            {
                dtstart = dt.AddDays(-daysOfweek + 1);
            }
            else if (day < daysOfweek && daysOfweek != 0)
            {
                dtstart = dt.AddDays(-day + 1);
            }
            else if (daysOfweek == 0 && day - 6 > 0)
            {
                dtstart = dt.AddDays(-6);
            }
            else if (daysOfweek == 0 && day - 6 <= 0)
            {
                dtstart = dt.AddDays(-day + 1);
            }

            ////////Find number of days that can display
            int y = dtstart.Year;
            int m = dtstart.Month;
            int d = dtstart.Day;
            int dayinmonth = DateTime.DaysInMonth(y, m);
            int daysOfweek2 = (int)dtstart.DayOfWeek;
            int numberOfdate = 0;
            if ((d + 7) > dayinmonth)
            {
                numberOfdate = dayinmonth - d + 1;
            }
            else
            {
                numberOfdate = 7 - daysOfweek2 + 1;
            }

            //data.Startdate = dtstart;
            //data.DayInWeek = (int)dtstart.DayOfWeek;  // Find day of week
            //data.NumberOfDate = numberOfdate;
            data.StartingDate = dtstart;
            data.DayInWeek= (int)dtstart.DayOfWeek;  // Find day of week
            data.NumberOfday = numberOfdate;
            return data;

        }

        public PpasStartEndDate FindStartEndDate(DateTime dt)
        {
            DateTime findday = new DateTime(dt.Year, dt.Month, dt.Day);
            int daysOfweek = (int)findday.DayOfWeek;
          
            DateTime startdate = daysOfweek !=0 ? findday.AddDays(-daysOfweek + 1): findday.AddDays(-6);
            
            DateTime enddate = startdate.AddDays(6);
            return new PpasStartEndDate()
            {
                StartDate = startdate,
                EndDate = enddate
            };
        }


    }

    public class PpasDisplayMember
    {
        public DateTime StartingDate;
        public int DayInWeek;
        public int NumberOfday;
    }
    public class PpasStartEndDate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}
