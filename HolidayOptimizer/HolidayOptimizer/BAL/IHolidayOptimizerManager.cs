using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimizer.BAL
{
    public interface IHolidayOptimizerManager
    {
        Task<IEnumerable<string>> GetCountryCodeWithMostHolidays(int year);
        Task<string> GetMostHolidaysMonth(int year);
        Task<IEnumerable<string>> GetUniqueHolidaysCountry(int year);
    }
}
