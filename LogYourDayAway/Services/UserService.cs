using LogYourDayAway.Models;

namespace LogYourDayAway.Services
{
    public class UserService : SyncDatabaseService<UserModel>
    {
        private readonly DatabaseHelper _database;

        public UserService(DatabaseHelper databaseHelper) : base(databaseHelper)
        {
            _database = databaseHelper;
        }

        public UserModel GetUserByUsername(string username)
        {
            var users = GetItems();
            return users.FirstOrDefault(u => u.Username == username);
        }

        public void SaveUser(UserModel user)
        {
            Add(user);
        }

        public bool ValidateUserCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            // Add your password validation logic here
            return user != null; // Placeholder
        }
    }
}