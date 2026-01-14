using SQLite;

namespace LogYourDayAway.Services
{
    public class DatabaseService<T> : IDatabase<T> where T : class, new()
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            //_database = new SQLiteAsyncConnection(DbSettings.DatabaseFilename, DbSettings.Flags);

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
            var prop = typeof(T).GetProperty("Id");
            if (prop != null && (int)prop.GetValue(item) != 0)
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
            var prop = typeof(T).GetProperty("Id");
            if (prop == null)
            {
                throw new InvalidOperationException($"Type {typeof(T).Name} does not contain a property named 'Id'.");
            }
            return await _database.Table<T>().Where(i => (int)prop.GetValue(i) == id).FirstOrDefaultAsync();
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
