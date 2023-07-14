namespace WeatherApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DetailsPage),typeof(DetailsPage));

        MainPage = new WeatherPage();
    }
}
