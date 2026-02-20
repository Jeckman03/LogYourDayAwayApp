using LogYourDayAway.ViewModel;

namespace LogYourDayAway;

public partial class RecoveryPage : ContentPage
{
	public RecoveryPage(RecoveryViewModel recoveryViewModel)
	{
		InitializeComponent();
		BindingContext = recoveryViewModel;
    }
}