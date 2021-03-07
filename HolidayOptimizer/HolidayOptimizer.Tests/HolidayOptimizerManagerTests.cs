using HolidayOptimizer.BAL;
using HolidayOptimizer.DataLayer;
using HolidayOptimizer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HolidayOptimizer.Tests
{
    [TestClass]
    public class HolidayOptimizerManagerTests
    {
        private HolidayOptimizerManager _manager;
        private Mock<IHolidayDataManager<Dictionary<string, string>, Dictionary<string, IEnumerable<HolidayDto>>>> _dataManager;

        public HolidayOptimizerManagerTests()
        {
            _dataManager = new Mock<IHolidayDataManager<Dictionary<string, string>, Dictionary<string, IEnumerable<HolidayDto>>>>();
            _manager = new HolidayOptimizerManager(_dataManager.Object);

            _dataManager.Setup(x => x.GetCountriesHolidaysByYear(It.IsAny<int>())).Returns(Task.FromResult(CreateMockData()));
        }

        private Dictionary<string, IEnumerable<HolidayDto>> CreateMockData()
        {
            var result = new Dictionary<string, IEnumerable<HolidayDto>>();

            result["QQ"] = new List<HolidayDto> { new HolidayDto { date = new DateTime(2020, 3, 3), countryCode = "QQ" }, new HolidayDto { date = new DateTime(2020, 2, 2), countryCode = "QQ" }, new HolidayDto{ date = new DateTime(2020, 1, 1), countryCode = "QQ" } };
            result["AA"] = new List<HolidayDto> { new HolidayDto { date = new DateTime(2020, 3, 3), countryCode = "AA" }, new HolidayDto { date = new DateTime(2020, 2, 2), countryCode = "AA" } };
            result["ZZ"] = new List<HolidayDto> { new HolidayDto { date = new DateTime(2020, 3, 3), countryCode = "ZZ" } };

            return result;
        }

        [TestMethod]
        public void GetCountryCodeWithMostHolidays_ShouldPass()
        {
            var expected = new List<string> {"QQ"};
            var result = _manager.GetCountryCodeWithMostHolidays(2020).Result.ToList();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetMostHolidaysMonth_ShouldPass()
        {
            var expected = "March";
            var result = _manager.GetMostHolidaysMonth(2020).Result;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetUniqueHolidaysCountry_ShouldPass()
        {
            var expected = new List<string> { "QQ" };
            var result = _manager.GetUniqueHolidaysCountry(2020).Result.ToList();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
