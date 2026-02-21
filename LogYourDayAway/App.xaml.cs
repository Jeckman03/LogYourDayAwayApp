using LogYourDayAway.Models;
using LogYourDayAway.Services;

namespace LogYourDayAway
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Add global exception handlers
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var exception = e.ExceptionObject as Exception;
                System.Diagnostics.Debug.WriteLine($"Unhandled Exception: {exception?.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {exception?.StackTrace}");
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Unobserved Task Exception: {e.Exception?.Message}");
                e.SetObserved();
            };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}