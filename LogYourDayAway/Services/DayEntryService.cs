using LogYourDayAway.Models;
using System.Diagnostics;

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
            var output = new List<DayEntryModel>();

            try
            {
                var allEntries = await _database.GetItemsAsync();

                output = allEntries
                    .Where(entry => entry.EntryDate.Month == date.Month && entry.EntryDate.Day == date.Day)
                    .ToList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error retrieving entries by day: " + ex.Message);
                throw;
            }

            return output;
        }

        public async Task<DayEntryModel?> GetLogForCurrentYear(DateTime date)
        { 
            var allEntries = await _database.GetItemsAsync();
            return allEntries
                .FirstOrDefault(entry => entry.EntryDate.Month == date.Month && entry.EntryDate.Day == date.Day && entry.EntryDate.Year == date.Year);
        }

        

    }
}
