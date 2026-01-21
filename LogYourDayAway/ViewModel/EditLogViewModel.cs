using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LogYourDayAway.ViewModel
{
    [QueryProperty(nameof(LogEntryId), "LogEntryId")]
    public partial class EditLogViewModel : BaseViewModel
    {
        private readonly IDatabase<DayEntryModel> _database;
        private readonly DayEntryService _dayEntryService;
        [ObservableProperty]
        private DayRank _selectedRank;

        [ObservableProperty]
        private DateTime _selectedDate;

        [ObservableProperty]
        private string _entryText;

        [ObservableProperty]
        private DayRank _selectedDayRank;

        [ObservableProperty]
        private int _logEntryId;

        private DayEntryModel CurrentEntry = new();
        public List<DayRank> DayRanks { get; } = Enum.GetValues(typeof(DayRank)).Cast<DayRank>().ToList();

        public EditLogViewModel(IDatabase<DayEntryModel> database, DayEntryService dayEntryService)
        {
            _database = database;
            _dayEntryService = dayEntryService;
        }

        partial void OnLogEntryIdChanged(int value)
        {
            LoadLogEntry();
        }

        [RelayCommand]
        private async Task LoadLogEntry()
        {
            IsBusy = true;

            try
            {
                CurrentEntry = await _database.GetByIdAsync(LogEntryId);

                SelectedDate = CurrentEntry.EntryDate;
                SelectedRank = CurrentEntry.DayRank;
                EntryText = CurrentEntry.EntryText;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load entry: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
