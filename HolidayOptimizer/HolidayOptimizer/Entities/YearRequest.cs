using System;
using System.ComponentModel.DataAnnotations;

namespace HolidayOptimizer.Entities
{
    public class YearRequest
    {
        [Range(0, 9999, ErrorMessage = "Year param should be in range (1, 9999)")]
        public int? Year { get; set; }
    }
}
