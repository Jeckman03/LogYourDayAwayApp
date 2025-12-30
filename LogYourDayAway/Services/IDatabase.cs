namespace LogYourDayAway.Services
{
    public interface IDatabase<T> where T : class
    {
        Task<List<T>> GetItemsAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);

    }
}
