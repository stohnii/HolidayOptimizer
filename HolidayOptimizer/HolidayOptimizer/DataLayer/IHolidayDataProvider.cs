using System.Threading.Tasks;

namespace HolidayOptimizer.DataLayer
{
    public interface IHolidayDataProvider<T>
    {
        Task<T> GetHolidaysByYear(int year);
    }
}
