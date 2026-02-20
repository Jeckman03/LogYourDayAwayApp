using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LogYourDayAway.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

namespace LogYourDayAway.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _errorMessage;

        [ObservableProperty]
        private string _password;
        private readonly DayEntryService _database;
        private readonly UserService _userService;
        private readonly DatabaseHelper _databaseHelper;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public LoginViewModel(DayEntryService database, UserService userService, DatabaseHelper databaseHelper)
        {
            _database = database;
            _userService = userService;
            _databaseHelper = databaseHelper;
        }

        [RelayCommand]
        private void ClearError() => ErrorMessage = string.Empty;

        [RelayCommand]
        private async Task Unlock()
        {
            if (IsBusy) return;
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = "Please enter a password.";
                    return;
                }

                string storedHash = await SecureStorage.Default.GetAsync("user_password_hash");
                string inputHash = HashString(Password);

                if (storedHash == inputHash)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    ErrorMessage = "Incorrect password.";
                    Password = string.Empty;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Something went wrong.";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        [SupportedOSPlatform("windows10.0.17763.0")]
        private async Task Reset()
        {
            string userInput = await Shell.Current.DisplayPromptAsync(
                "Factory Reset",
                "This will permanently delete ALL journal entries and passwords. This cannot be undone. Type DELETE to confirm.",
                accept: "Confirm",
                cancel: "Cancel",
                placeholder: "Type DELETE here",
                maxLength: 6);

            if (string.IsNullOrWhiteSpace(userInput) || userInput.Trim().ToUpper() != "DELETE")
                {
                    await Shell.Current.DisplayAlertAsync(
                        "Reset Cancelled",
                        "Factory reset has been cancelled.",
                        "OK");
                return;
            }

            try
            {
                // Clear all secure storage (passwords, recovery codes)
                SecureStorage.Default.RemoveAll();

                // Delete the entire database
                await _databaseHelper.FactoryResetAsync();

                await Shell.Current.GoToAsync("SetupPage");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    $"Could not complete reset: {ex.Message}",
                    "OK");
            }
        }
        

        [RelayCommand]
        [SupportedOSPlatform("windows10.0.17763.0")]
        private async Task ForgotPassword()
        {
            string input = await Shell.Current.DisplayPromptAsync("Recover Account",
                                                                 "Enter your recovery code (XXXX-XXXX-XXXX):",
                                                                 maxLength: 14);

            if (string.IsNullOrWhiteSpace(input)) return;

            string normalizedInput = input.ToUpper().Trim();

            string storedHash = await SecureStorage.Default.GetAsync("recovery_code_hash");
            string inputHash = HashString(normalizedInput);

            if (storedHash == inputHash)
            {
                await Shell.Current.GoToAsync(nameof(ResetPasswordPage));
            }
            else
            {
                ErrorMessage = "Invalid recovery code";
            }
        }

        private string HashString(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
