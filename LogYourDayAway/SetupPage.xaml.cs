using LogYourDayAway.ViewModel;

namespace LogYourDayAway;

public partial class SetupPage : ContentPage
{
	public SetupPage(SetupViewModel setupViewModel)
	{
		InitializeComponent();
		BindingContext = setupViewModel;
    }
}