using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LogYourDayAway.ViewModel
{
    public partial class SetupViewModel : BaseViewModel
    {
        private readonly UserRepository _userRepository;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordMatch))]
        private string _confirmPassword;

        [ObservableProperty]
        private string _errorMessage;

        public bool PasswordMatch => Password == ConfirmPassword;

        public SetupViewModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [RelayCommand]
        private async Task CreateJournal()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please fill in all fields.";
                    return;
                }
                if (!PasswordMatch)
                {
                    ErrorMessage = "Passwords do not match.";
                    return;
                }

                string hash = HashString(Password);
                await SecureStorage.Default.SetAsync("user_password_hash", hash);

                var newUser = new UserModel
                {
                    Username = Username
                };

                await _userRepository.SaveUserAsync(newUser);

                await Shell.Current.GoToAsync("MainPage");

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Setup failed: {ex.Message}";
            }

        }

        private string HashString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash =sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
