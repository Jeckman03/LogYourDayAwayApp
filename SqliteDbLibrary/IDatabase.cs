namespace SqliteDbLibrary
{
    public interface IDatabase<T> where T : class
    {
        Task<List<T>> GetItemsAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        Task DeleteAsync(T item);
    }
}
