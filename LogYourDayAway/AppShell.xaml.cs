namespace LogYourDayAway
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LogDayView", typeof(LogDayView));
            Routing.RegisterRoute("EditLogView", typeof(EditLogView));
        }
    }
}
