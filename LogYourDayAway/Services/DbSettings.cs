using SQLite;

namespace LogYourDayAway.Services
{
    public class DbSettings
    {
        public const string DatabaseFilename = "db.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DatabaseFilename);

        public static SQLiteAsyncConnection OpenDatabase(string fileName = "db.db3")
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, fileName);
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Ensure file exists or allow creation by flags
            var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            try
            {
                var conn = new SQLiteAsyncConnection(fullPath, flags);
                return conn;
            }
            catch (SQLiteException ex)
            {
                // Log fullPath and ex.Message/StackTrace before rethrowing
                throw;
            }
        }
    }
}
