using SQLite;

namespace LogYourDayAway.Services
{
    public class DatabaseService<T> : IDatabase<T> where T : class, IEntity, new()
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {

            _database = DbSettings.OpenDatabase();
        }

        async Task Init()
        {
            if (_database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
                return;
            await _database.CreateTableAsync<T>().ConfigureAwait(false);
        }

        public async Task AddAsync(T item)
        {
            await Init();
            if (item.Id != 0)
            {
                await _database.UpdateAsync(item);
            }
            else
            {
                await _database.InsertAsync(item);
            }
        }

        public async Task DeleteAsync(T item)
        {
            await Init();
            await _database.DeleteAsync(item);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            await Init();
            try
            {
                var output = await _database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();

                return output;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Did not find a record");
                throw;
            }
        }

        public async Task<List<T>> GetItemsAsync()
        {
            await Init();
            return await _database.Table<T>().ToListAsync();
        }

        public async Task UpdateAsync(T item)
        {
            await Init();
            await _database.UpdateAsync(item);
        }
    }
}
