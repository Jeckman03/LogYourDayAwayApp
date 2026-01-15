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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LogDayView>();

            // ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LogDayViewModel>();


            // Services
            builder.Services.AddTransient<DayEntryService>();
            builder.Services.AddSingleton<IDatabase<DayEntryModel>, DatabaseService<DayEntryModel>>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
