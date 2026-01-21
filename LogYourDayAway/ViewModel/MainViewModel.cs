using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LogYourDayAway.Messages;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using System.Collections.ObjectModel;
using LogYourDayAway.Messages;
using System.Diagnostics;

namespace LogYourDayAway.ViewModel
{
    public partial class MainViewModel : BaseViewModel
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

        partial void OnSelectedDateChanged(DateTime value)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                SelectedDate = value;
                LoadEntriesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error loading entries for selected date: " + ex.Message);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void NextDay()
        {
            IsBusy = true;

            try
            {
                SelectedDate = SelectedDate.AddDays(1);
                LoadEntriesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void PreviousDay()
        {
            IsBusy = true;

            try
            {
                SelectedDate = SelectedDate.AddDays(-1);
                LoadEntriesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async void LogDay()
        {
            IsBusy = true;

            try
            {
                var existingLog = await _dayEntryService.GetLogForCurrentYear(SelectedDate);

                if (existingLog != null)
                {
                    bool answer = await Shell.Current.DisplayAlertAsync("Existing Log Found",
                        "A log for this date already exists. Do you want to edit it?",
                        "Yes", "No");

                    if (answer)
                    {
                        // Navigate to edit existing log
                        await Shell.Current.GoToAsync("EditLogView", true, new Dictionary<string, object>
                        {
                            { "LogEntryId", existingLog.Id }
                        });
                    }
                    else
                    {
                        return;
                    }
                }
                else 
                {
                    await Shell.Current.GoToAsync("LogDayView", true, new Dictionary<string, object>
                    {
                        { "SelectedDate", SelectedDate }
                    });
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error navigating to LogDayView: " + ex.Message);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
