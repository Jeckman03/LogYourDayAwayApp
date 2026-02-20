using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogYourDayAway.Services
{
    public class SyncDatabaseService<T> where T : class, IEntity, new()
    {
        private readonly DatabaseHelper _databaseHelper;

        protected SQLiteConnection _db => _databaseHelper.GetSyncConnection();

        public SyncDatabaseService(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        void Init()
        {
            try
            {
                var tableInfo = _db.GetTableInfo(typeof(T).Name);
                if (tableInfo.Any())
                {
                    return;
                }
            }
            catch
            {

            }

            _db.CreateTable<T>();
        }

        public void Add(T item)
        {
            Init();
            if (item.Id != 0)
            {
                _db.Update(item);
            }
            else
            {
                _db.Insert(item);
            }
        }

        public void Delete(T item)
        {
            Init();
            _db.Delete(item);
        }

        public T GetById(int id)
        {
            Init();
            try
            {
                var output = _db.Table<T>().Where(i => i.Id == id).FirstOrDefault();

                return output;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Did not find a record");
                throw;
            }
        }

        public List<T> GetItems()
        {
            Init();
            return _db.Table<T>().ToList();
        }

        public void Update(T item)
        {
            Init();
            _db.Update(item);
        }
    }
}
