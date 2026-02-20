using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LogYourDayAway.ViewModel
{
    [QueryProperty(nameof(RecoveryCode), "code")]
    public partial class RecoveryViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string recoveryCode;

        [RelayCommand]
        private async Task ConfirmSaved()
        {
            string hash = HashString(RecoveryCode);
            await SecureStorage.Default.SetAsync("recovery_code_hash", hash);

            await Shell.Current.GoToAsync("//MainPage");
        }

        private string HashString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
