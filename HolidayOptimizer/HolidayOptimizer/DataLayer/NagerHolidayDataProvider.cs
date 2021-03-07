using HolidayOptimizer.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HolidayOptimizer.DataLayer
{
    public class NagerHolidayDataProvider : IHolidayDataProvider<Dictionary<string, IEnumerable<HolidayDto>>>
    {
        private string _url = "https://date.nager.at/api/v2/publicholidays/{0}/{1}"; // 0 - year, 1 - countryCode
        private List<string> _availableCountryCodes = new List<string>{"AD", "AR", "AT", "AU", "AX",
            "BB", "BE", "BG", "BO", "BR", "BS", "BW", "BY", "BZ", "CA", "CH", "CL", "CN", "CO", "CR", "CU", "CY",
            "CZ", "DE", "DK", "DO", "EC", "EE", "EG", "ES", "FI", "FO", "FR", "GA", "GB", "GD", "GL", "GR", "GT",
            "GY", "HN", "HR", "HT", "HU", "IE", "IM", "IS", "IT", "JE", "JM", "LI", "LS", "LT", "LU", "LV", "MA",
            "MC", "MD", "MG", "MK", "MT", "MX", "MZ", "NA", "NI", "NL", "NO", "NZ", "PA", "PE", "PL", "PR", "PT", 
            "PY", "RO", "RS", "RU", "SE", "SI", "SJ", "SK", "SM", "SR", "SV", "TN", "TR", "UA", "US", "UY", "VA", 
            "VE", "ZA" };

        public async Task<Dictionary<string, IEnumerable<HolidayDto>>> GetHolidaysByYear(int year)
        {
            var result = new Dictionary<string, IEnumerable<HolidayDto>>(_availableCountryCodes.Count);

            using (HttpClient client = new HttpClient())
            {
                foreach (string countryCode in _availableCountryCodes)
                {
                    var streamTask = await client.GetStreamAsync(String.Format(_url, year, countryCode));
                    var holidays = await System.Text.Json.JsonSerializer.DeserializeAsync<List<HolidayDto>>(streamTask);
                    result[countryCode] = holidays;
                }
            }

            return result;
        }
    }
}
