using SQLite;

namespace LogYourDayAway.Services
{
    public class DbSettings
    {
        public static string GetDatabasePath(string fileName = "db.db3")
        {
            // Use FileSystem.AppDataDirectory for a more appropriate location on mobile platforms
            string baseDir = FileSystem.AppDataDirectory;
            return Path.Combine(baseDir, fileName);
        }

        public static SQLiteAsyncConnection OpenDatabase(string fileName = "db.db3")
        {
            var fullPath = GetDatabasePath(fileName);
            var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            return new SQLiteAsyncConnection(fullPath, flags);
        }

        public static SQLiteConnection OpenSynchronousDatabase(string fileName = "db.db3")
        {
            var fullPath = GetDatabasePath(fileName);
            var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            return new SQLiteConnection(fullPath, flags);
        }

        //public static SQLiteAsyncConnection OpenDatabase(string fileName = "db.db3")
        //{
        //    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        //    var fullPath = Path.Combine(baseDir, fileName);
        //    var dir = Path.GetDirectoryName(fullPath);
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    // Ensure file exists or allow creation by flags
        //    var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        //    try
        //    {
        //        var conn = new SQLiteAsyncConnection(fullPath, flags);
        //        return conn;
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        // Log fullPath and ex.Message/StackTrace before rethrowing
        //        throw;
        //    }
        //}
    }
}
