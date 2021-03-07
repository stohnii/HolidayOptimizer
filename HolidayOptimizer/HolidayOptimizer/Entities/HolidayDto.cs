using System;
using System.Collections.Generic;

namespace HolidayOptimizer.Entities
{
    public class HolidayDto
    {
        public DateTime date { get; set; }
        public string localName { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public bool @fixed { get; set; }
        public bool global { get; set; }
        public List<string> counties { get; set; }
        public ushort? lunchYear { get; set; } 
    }
}
