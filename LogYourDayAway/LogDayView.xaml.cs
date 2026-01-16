using LogYourDayAway.ViewModel;

namespace LogYourDayAway;

public partial class LogDayView : ContentPage
{

    public LogDayView(LogDayViewModel logDayViewModel)
    {
        InitializeComponent();
        BindingContext = logDayViewModel;
    }
}