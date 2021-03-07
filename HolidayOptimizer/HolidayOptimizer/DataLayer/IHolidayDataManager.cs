using HolidayOptimizer.Cache;
using System.Threading.Tasks;

namespace HolidayOptimizer.DataLayer
{
    public interface IHolidayDataManager<R, T>
    {
        Task<T> GetCountriesHolidaysByYear(int year);
    }
}
