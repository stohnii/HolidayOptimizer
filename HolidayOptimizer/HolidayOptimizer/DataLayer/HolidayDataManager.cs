using HolidayOptimizer.Cache;
using HolidayOptimizer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HolidayOptimizer.DataLayer
{
    public class HolidayDataManager : IHolidayDataManager<Dictionary<string, string>, Dictionary<string, IEnumerable<HolidayDto>>>
    {
        private readonly ICacheProvider<Dictionary<string, string>> _cache;
        private readonly IHolidayDataProvider<Dictionary<string, IEnumerable<HolidayDto>>> _dataProvider;

        public HolidayDataManager(ICacheProvider<Dictionary<string, string>> cache, 
            IHolidayDataProvider<Dictionary<string, IEnumerable<HolidayDto>>> dataProvider)
        {
            _cache = cache;
            _dataProvider = dataProvider;
        }

        public async Task<Dictionary<string, IEnumerable<HolidayDto>>> GetCountriesHolidaysByYear(int year)
        {
            Dictionary<string, IEnumerable<HolidayDto>> holidays;

            if (!_cache.KeyExists($"{year}"))
            {
                holidays = await _dataProvider.GetHolidaysByYear(year);
                _cache.AddOrUpdate(holidays.ToDictionary(kv => kv.Key, kv => JsonSerializer.Serialize(kv.Value)), $"{year}");
            }
            else 
            {
                holidays = _cache.GetCached($"{year}").ToDictionary(kv => kv.Key, kv => JsonSerializer.Deserialize<IEnumerable<HolidayDto>>(kv.Value));
            }

            return holidays;
        }
    }
}
