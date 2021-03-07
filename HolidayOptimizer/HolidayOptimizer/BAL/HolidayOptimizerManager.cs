using HolidayOptimizer.DataLayer;
using HolidayOptimizer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimizer.BAL
{
    public class HolidayOptimizerManager : IHolidayOptimizerManager
    {
        private readonly IHolidayDataManager<Dictionary<string, string>, Dictionary<string, IEnumerable<HolidayDto>>> _dataManager;

        public HolidayOptimizerManager(IHolidayDataManager<Dictionary<string, string>, Dictionary<string, IEnumerable<HolidayDto>>> dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IEnumerable<string>> GetCountryCodeWithMostHolidays(int year)
        {
            var data = await _dataManager.GetCountriesHolidaysByYear(year);
            int maxHolidays = data.Max(kv => kv.Value.Count());
            return data.Where(kv => kv.Value.Count() == maxHolidays).Select(kv => kv.Key);
        }

        public async Task<string> GetMostHolidaysMonth(int year)
        {
            var data = await _dataManager.GetCountriesHolidaysByYear(year);
            var holidaysPerMonth = new Dictionary<int, int>(12);
            
            foreach (var country in data.Values)
            {
                foreach (var holiday in country)
                {
                    if (!holidaysPerMonth.ContainsKey(holiday.date.Month))
                        holidaysPerMonth[holiday.date.Month] = 0;

                    holidaysPerMonth[holiday.date.Month]++;
                }
            }

            return CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(holidaysPerMonth.OrderByDescending(kv => kv.Value).First().Key);
        }

        public async Task<IEnumerable<string>> GetUniqueHolidaysCountry(int year)
        {
            var data = await _dataManager.GetCountriesHolidaysByYear(year);
            
            var holidayInCountries = new Dictionary<DateTime, List<string>>();
            foreach (var country in data.Values)
            {
                foreach (var holiday in country)
                {
                    if (!holidayInCountries.ContainsKey(holiday.date.Date))
                        holidayInCountries[holiday.date.Date] = new List<string>();

                    holidayInCountries[holiday.date.Date].Add(holiday.countryCode);
                }
            }

            var uniqueHolidaysInCountries = new Dictionary<string, int>();
            foreach (var holiday in holidayInCountries.Where(kv => kv.Value.Count == 1))
            {
                var countryCode = holiday.Value.First();
                if (!uniqueHolidaysInCountries.ContainsKey(countryCode))
                    uniqueHolidaysInCountries[countryCode] = 0;

                uniqueHolidaysInCountries[countryCode]++;
            }

            int maxUniqueHolidays = uniqueHolidaysInCountries.Max(kv => kv.Value);
            return uniqueHolidaysInCountries.Where(kv => kv.Value == maxUniqueHolidays).Select(kv => kv.Key);
        }
    }
}
