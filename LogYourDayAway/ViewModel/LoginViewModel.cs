using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

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
                    await Shell.Current.GoToAsync("MainPage");
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
        private async Task Reset()
        {
            bool confirm = await Shell.Current.DisplayAlertAsync("Reset Journal?", "This will delete all your local data. Are you sure?", "Yes, Delete Everything", "Cancel");

            if (confirm)
            {
                SecureStorage.Default.RemoveAll();
                await Shell.Current.GoToAsync("SetupPage");
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
