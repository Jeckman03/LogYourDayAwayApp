namespace LogYourDayAway
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LogDayView", typeof(LogDayView));
        }
    }
}
