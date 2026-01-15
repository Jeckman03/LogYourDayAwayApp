using LogYourDayAway.Models;

namespace LogYourDayAway.Services
{
    public class DayEntryService
    {
        private readonly IDatabase<DayEntryModel> _database;

        public DayEntryService(IDatabase<DayEntryModel> database)
        {
            _database = database;
        }

        public async Task<List<DayEntryModel>> GetEntryLogsByDay(DateTime date)
        {
            var allEntries = await _database.GetItemsAsync();

            var output = allEntries
                .Where(entry => entry.EntryDate.Month == date.Month && entry.EntryDate.Day == date.Day)
                .ToList();

            return output;
        }

    }
}
