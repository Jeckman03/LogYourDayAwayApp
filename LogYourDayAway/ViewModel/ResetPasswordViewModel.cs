using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LogYourDayAway.ViewModel
{
    public partial class ResetPasswordViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _newPassword;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordsMatch))]
        private string _confirmPassword;

        [ObservableProperty]
        private string _errorMessage;

        public bool PasswordsMatch => NewPassword == ConfirmPassword;

        [RelayCommand]
        private async Task SaveNewPassword()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewPassword))
                {
                    ErrorMessage = "Password cannot be empty";
                    return;
                }

                if (!PasswordsMatch)
                {
                    ErrorMessage = "Passwords do not match";
                    return;
                }

                string newHash = HashString(NewPassword);

                
                await SecureStorage.Default.SetAsync("user_password_hash", newHash);

                await Shell.Current.DisplayAlertAsync("Success", "Your password has been reset", "Open Journal");
                await Shell.Current.GoToAsync("MainPage");
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to save password. Please try again.";
            }
        }

        // Helper (Keep this consistent with your other ViewModels)
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
