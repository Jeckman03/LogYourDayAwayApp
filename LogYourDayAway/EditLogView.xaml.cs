using LogYourDayAway.ViewModel;

namespace LogYourDayAway;

public partial class EditLogView : ContentPage
{
	public EditLogView(EditLogViewModel editLogViewModel)
	{
		InitializeComponent();
		BindingContext = editLogViewModel;
    }
}