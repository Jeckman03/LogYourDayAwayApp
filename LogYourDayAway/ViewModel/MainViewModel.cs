using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LogYourDayAway.Messages;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using System.Collections.ObjectModel;
using LogYourDayAway.Messages;

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
        private readonly DayEntryService _dayEntryService;

        public MainViewModel(IDatabase<DayEntryModel> database, DayEntryService dayEntryService)
        {
            _database = database;
            _dayEntryService = dayEntryService;
            LoadEntriesAsync();

            WeakReferenceMessenger.Default.Register<LogSavedMessage>(this, (r, message) =>
            {
                LoadEntriesAsync();
            });
        }

        private async void LoadEntriesAsync()
        {
            var list = await _dayEntryService.GetEntryLogsByDay(SelectedDate);
            Entries = new ObservableCollection<DayEntryModel>(list);
        }

        [RelayCommand]
        private void NextDay()
        {
            SelectedDate = SelectedDate.AddDays(1);
            LoadEntriesAsync();
        }

        [RelayCommand]
        private void PreviousDay()
        {
            SelectedDate = SelectedDate.AddDays(-1);
            LoadEntriesAsync();
        }

        [RelayCommand]
        private async void LogDay()
        {
            //string textEntry = await Shell.Current.DisplayPromptAsync("Log Day", "Enter your day", "OK", "Cancel");
            //if (!string.IsNullOrWhiteSpace(textEntry))
            //{
            //    DayEntryModel newLog = new DayEntryModel { EntryDate = SelectedDate, EntryText = textEntry };
            //    Entries.Add(newLog);

            //    await _database.AddAsync(newLog);

            //}

            await Shell.Current.GoToAsync("LogDayView", true, new Dictionary<string, object>
            {
                { "SelectedDate", SelectedDate }
            });
        }
    }
}
