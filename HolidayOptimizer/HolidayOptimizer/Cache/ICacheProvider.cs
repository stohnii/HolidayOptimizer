namespace HolidayOptimizer.Cache
{
    public interface ICacheProvider<T>
    {
        T GetCached(string key);
        void AddOrUpdate(T data, string key);
        bool KeyExists(string key);
    }
}
