using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LogYourDayAway.Messages;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using System.Diagnostics;

namespace LogYourDayAway.ViewModel
{
    [QueryProperty(nameof(SelectedDate), "SelectedDate")]
    public partial class LogDayViewModel : ObservableObject
    {
        
        private readonly IDatabase<DayEntryModel> _database;
        public List<DayRank> DayRanks { get; } = Enum.GetValues(typeof(DayRank)).Cast<DayRank>().ToList();

        [ObservableProperty]
        private DayRank _selectedRank;

        [ObservableProperty]
        private DateTime _selectedDate;

        [ObservableProperty]
        private string _entryText;

        //[ObservableProperty]
        //private DayRank _selectedDayRank;


        public LogDayViewModel(IDatabase<DayEntryModel> database)
        {
            _database = database;
        }



        [RelayCommand]
        private async Task SaveLog()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(EntryText))
                {
                    DayEntryModel newLog = new DayEntryModel
                    {
                        EntryDate = SelectedDate,
                        EntryText = EntryText,
                        DayRank = SelectedRank
                    };
                    await _database.AddAsync(newLog);

                    WeakReferenceMessenger.Default.Send(new LogSavedMessage());

                    await Shell.Current.GoToAsync("..");


                }
                else
                {
                    Debug.WriteLine("Entry text is empty. Log not saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Could not save log to database: {ex}");
                throw;
            }
        }
    }
}
