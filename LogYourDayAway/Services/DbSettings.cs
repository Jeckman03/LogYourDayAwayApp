using SQLite;

namespace LogYourDayAway.Services
{
    public class DbSettings
    {
        public static string GetDatabasePath(string fileName = "db.db3")
        {
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
    }
}
