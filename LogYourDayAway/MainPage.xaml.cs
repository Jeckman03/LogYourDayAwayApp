using LogYourDayAway.ViewModel;

namespace LogYourDayAway
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }


    }
}
