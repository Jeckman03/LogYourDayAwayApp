using CommunityToolkit.Maui;
using LogYourDayAway.Models;
using LogYourDayAway.Services;
using LogYourDayAway.ViewModel;
using Microsoft.Extensions.Logging;

namespace LogYourDayAway
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Views
            builder.Services.AddTransient<SetupPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LogDayView>();
            builder.Services.AddTransient<EditLogView>();
            builder.Services.AddTransient<RecoveryPage>();
            builder.Services.AddTransient<ResetPasswordPage>();

            // ViewModels
            builder.Services.AddTransient<SetupViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LogDayViewModel>();
            builder.Services.AddTransient<EditLogViewModel>();
            builder.Services.AddTransient<RecoveryViewModel>();
            builder.Services.AddTransient<ResetPasswordViewModel>();


            // Services
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<DayEntryService>();
            builder.Services.AddSingleton<DatabaseHelper>();
            builder.Services.AddSingleton<IDatabase<DayEntryModel>, DayEntryService>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
