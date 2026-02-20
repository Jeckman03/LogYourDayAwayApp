using LogYourDayAway.Models;
using System.Diagnostics;

namespace LogYourDayAway.Services
{
    public class DayEntryService : AsyncDatabaseService<DayEntryModel>
    {
        private readonly DatabaseHelper _databaseHelper;

        public DayEntryService(DatabaseHelper databaseHelper) : base(databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<List<DayEntryModel>> GetEntryLogsByDay(DateTime date)
        {
            var output = new List<DayEntryModel>();

            try
            {
                var allEntries = await GetItemsAsync();

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
            var allEntries = await GetItemsAsync();
            return allEntries
                .FirstOrDefault(entry => entry.EntryDate.Month == date.Month && entry.EntryDate.Day == date.Day && entry.EntryDate.Year == date.Year);
        }

        // Is this a future date
        public bool IsFutureDate(DateTime date)
        {
            return date.Date > DateTime.Now.Date;
        }

        public async Task DeleteEntryAsync(DayEntryModel entry)
        {
            try
            {
                await _db.DeleteAsync(entry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error deleting entry: " + ex.Message);
                throw;
            }
        }
    }
}
