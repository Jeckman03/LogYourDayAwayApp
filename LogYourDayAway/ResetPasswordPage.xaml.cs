using LogYourDayAway.ViewModel;

namespace LogYourDayAway;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage(ResetPasswordViewModel resetPasswordViewModel)
	{
		InitializeComponent();
		BindingContext = resetPasswordViewModel;
    }
}