using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RememberCalendar
{
    internal class CalendarAddress
    {
        public CalendarAddress(string baseUrl, string relativeUrl)
        {
            this.baseUrl = baseUrl;
            this.relativeUrl = relativeUrl;
        }
        public string FullUrl()
        {
            return baseUrl + relativeUrl;
        }
        public string baseUrl { get; set; }
        public string relativeUrl { get; set; }
    }
}
