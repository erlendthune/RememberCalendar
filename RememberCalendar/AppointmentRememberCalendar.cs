using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ical.Net;

namespace RememberCalendar
{
    public class AppointmentRememberCalendar
    {
        private Ical.Net.DataTypes.IDateTime when;
        public string Summary { get; set; }

        public AppointmentRememberCalendar(string summary, Ical.Net.DataTypes.IDateTime when)
        {
            Summary = summary;
            When = when;
        }

        public bool AppointmentComingUpSoon(int minutes)
        {
            var alertTime = When.AddMinutes(-minutes);
            if (DateTime.Now.Ticks > alertTime.Ticks)
            {
                return true;
            }
            return false;
        }


        public Ical.Net.DataTypes.IDateTime When { get => when; set => when = value; }
    }
}
