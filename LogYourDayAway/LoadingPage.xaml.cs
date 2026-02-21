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

		try
		{
			// Add a small delay to ensure the page is fully loaded
			await Task.Delay(100);

			bool userExists = await Task.Run(() =>
			{
				try
				{
					using (var conn = DbSettings.OpenSynchronousDatabase())
					{
						conn.CreateTable<UserModel>();
						return conn.Table<UserModel>().Count() > 0;
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
					return false;
				}
			});

			// Use absolute navigation
			if (userExists)
			{
				await Shell.Current.GoToAsync("//LoginPage");
			}
			else
			{
				await Shell.Current.GoToAsync("SetupPage");
			}
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
			// Fallback navigation
			await Shell.Current.GoToAsync("SetupPage");
		}
	}
}