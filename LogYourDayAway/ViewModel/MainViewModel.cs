using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using System.Collections.ObjectModel;

namespace LogYourDayAway.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DayEntryModel> entries = new ObservableCollection<DayEntryModel>();

        [ObservableProperty]
        private DayEntryModel selectedEntry;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        private readonly IDatabase<DayEntryModel> _database;

        public MainViewModel(IDatabase<DayEntryModel> database)
        {
            _database = database;

            GetEntries();
        }

        private async void GetEntries()
        {
            var allItems = await _database.GetItemsAsync();

            var output = allItems.Where(x => x.EntryDate.Month == SelectedDate.Month && x.EntryDate.Day == SelectedDate.Day).ToList();

            Entries = new ObservableCollection<DayEntryModel>(output);
        }

        [RelayCommand]
        private void NextDay()
        {
            SelectedDate = SelectedDate.AddDays(1);
            GetEntries();
        }

        [RelayCommand]
        private void PreviousDay()
        {
            SelectedDate = SelectedDate.AddDays(-1);
            GetEntries();
        }

        [RelayCommand]
        private async void LogDay()
        {
            string textEntry = await Shell.Current.DisplayPromptAsync("Log Day", "Enter your day", "OK", "Cancel");
            if (!string.IsNullOrWhiteSpace(textEntry))
            {
                DayEntryModel newLog = new DayEntryModel { EntryDate = SelectedDate, EntryText = textEntry };
                Entries.Add(newLog);

                await _database.AddAsync(newLog);

            }
        }
    }
}
