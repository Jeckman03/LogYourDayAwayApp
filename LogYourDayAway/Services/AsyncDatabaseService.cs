using SQLite;

namespace LogYourDayAway.Services
{
    public class AsyncDatabaseService<T> : IDatabase<T> where T : class, IEntity, new()
    {
        private readonly DatabaseHelper _databaseHelper;
        protected SQLiteAsyncConnection _db => _databaseHelper.GetAsyncConnection();

        public AsyncDatabaseService(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        async Task Init()
        {
            try
            {
                var tableInfo = await _db.GetTableInfoAsync(typeof(T).Name);
                if (tableInfo.Any())
                {
                    return;
                }
            }
            catch
            {

            }

            await _db.CreateTableAsync<T>().ConfigureAwait(false);
        }

        public async Task AddAsync(T item)
        {
            await Init();
            if (item.Id != 0)
            {
                await _db.UpdateAsync(item);
            }
            else
            {
                await _db.InsertAsync(item);
            }
        }

        public async Task DeleteAsync(T item)
        {
            await Init();
            await _db.DeleteAsync(item);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            await Init();
            try
            {
                var output = await _db.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();

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
            return await _db.Table<T>().ToListAsync();
        }

        public async Task UpdateAsync(T item)
        {
            await Init();
            await _db.UpdateAsync(item);
        }


    }
}
