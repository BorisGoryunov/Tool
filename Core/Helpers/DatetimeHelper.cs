using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class DatetimeHelper
    {
        public static int Age(DateTime birthdate, DateTime now)
        {
            int age = now.Year - birthdate.Year;
            if (birthdate > now.AddYears(-age))
            {
                age--;
            }
            if (age < 0)
            {
                age = 0;
            }
            return age;
        }

        public static TimeSpan BetweenDateTime(DateTime now, DateTime when)
        {
            return now - when;
        }

        public static DateTime BeginHour(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0);
        }

        public static DateTime BeginHour()
        {
            return BeginHour(Now);
        }

        public static DateTime Now
        {
            get
            {
                var result = DateTime.Now;
#if DEBUG
                //result = new DateTime(2017, 3, 4, 18, 0, 0);
                //result = result.AddDays(-60).AddHours(-1);
                //result = result.AddHours(-10);
#endif
                return result;
            }
        }
    }
}
