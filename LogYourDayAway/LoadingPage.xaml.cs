using LogYourDayAway.Models;
using LogYourDayAway.Services;
using System.Threading.Tasks;

namespace LogYourDayAway;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		bool userExists = await Task.Run(() =>
		{
			using (var conn = DbSettings.OpenSynchronousDatabase())
			{
				conn.CreateTable<UserModel>();
				return conn.Table<UserModel>().Count() > 0;
			}
		});

		if (userExists)
		{
			await Shell.Current.GoToAsync("LoginPage");
		}
		else
		{
			await Shell.Current.GoToAsync("SetupPage");
		}
    }
}