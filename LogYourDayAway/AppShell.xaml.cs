namespace LogYourDayAway
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("SetupPage", typeof(SetupPage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("LogDayView", typeof(LogDayView));
            Routing.RegisterRoute("EditLogView", typeof(EditLogView));
        }
    }
}
