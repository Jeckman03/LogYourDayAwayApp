using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogYourDayAway.Services
{
    public class DatabaseHelper
    {
        private SQLiteAsyncConnection _asyncConnection;
        private SQLiteConnection _syncConnection;

        public SQLiteAsyncConnection GetAsyncConnection() => _asyncConnection ??= DbSettings.OpenDatabase();

        public SQLiteConnection GetSyncConnection() => _syncConnection ??= DbSettings.OpenSynchronousDatabase();

        public async Task FactoryResetAsync()
        {
            if (_asyncConnection != null)
            {
                await _asyncConnection.CloseAsync();
            }

            _syncConnection?.Close();

            SQLiteAsyncConnection.ResetPool();

            var path = DbSettings.GetDatabasePath();
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            _asyncConnection = null;
            _syncConnection = null;

        }
    }
}
