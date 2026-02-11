using LogYourDayAway.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogYourDayAway.Services
{
    public class UserRepository
    {

        SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database != null)
                return;
            _database = DbSettings.OpenDatabase();
            await _database.CreateTableAsync<UserModel>();
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            await Init();
            return await _database.Table<UserModel>().ToListAsync();
        }

        public async Task<UserModel> GetUserByUserNameAsync(string userName)
        {
            await Init();
            return await _database.Table<UserModel>()
                .Where(u => u.Username == userName)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserAsync(UserModel user)
        {
            await Init();
            if (user.Id != 0)
            {
                return await _database.UpdateAsync(user);
            }
            else
            {
                return await _database.InsertAsync(user);
            }
        }
    }
}
