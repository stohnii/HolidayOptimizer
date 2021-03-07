using HolidayOptimizer.BAL;
using HolidayOptimizer.Entities;
using HolidayOptimizer.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HolidayOptimizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class HolidayController : ControllerBase
    {
        public IHolidayOptimizerManager _holidayOptimizerManager;

        public HolidayController(IHolidayOptimizerManager holidayOptimizerManager)
        {
            _holidayOptimizerManager = holidayOptimizerManager;
        }

        //Which country had the most holidays this year
        [HttpGet("mostholidayscountry")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCountryCode([FromQuery]YearRequest model = null)
        {
            var result = await _holidayOptimizerManager.GetCountryCodeWithMostHolidays(model?.Year ?? DateTime.Now.Year);
            return Ok(result);
        }

        //Which month had most holidays if you compare globally?
        [HttpGet("mostholidaysmonth")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMonth([FromQuery] YearRequest model = null)
        {
            string result = await _holidayOptimizerManager.GetMostHolidaysMonth(model?.Year ?? DateTime.Now.Year);
            return Ok(result);
        }

        //Which country had the most unique holidays? E.g.days that no other country had a holiday.
        [HttpGet("uniqueholidayscountry")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUniqueHolidaysCountry([FromQuery] YearRequest model = null)
        {
            var result = await _holidayOptimizerManager.GetUniqueHolidaysCountry(model?.Year ?? DateTime.Now.Year);
            return Ok(result);
        }
    }
}
