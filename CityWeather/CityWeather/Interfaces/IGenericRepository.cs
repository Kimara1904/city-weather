namespace CityWeather.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T?> FindAsync(int id);
        Task Insert(T entity);
        Task RemoveAllAsync();
    }
}
