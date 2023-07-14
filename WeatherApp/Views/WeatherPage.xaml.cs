using System.Reflection.Metadata;
using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;
    public WeatherPage()
    {
        InitializeComponent();
        WeatherList = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude,longitude);

    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
       var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search Weather By city", accept: "Search", cancel: "Cancel");
        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.getWeatherByCity(city);
        updateUI(result);
 
    }


    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.getWeather(latitude, longitude);
        updateUI(result);
    }

    public void updateUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;

        Lcity.Text = result.city.name;
        LbWeatherDescription.Text = result.list[0].weather[0].description;
        LbTemperature.Text = result.list[0].main.round_temperature + "°C";
        LbHumidity.Text = result.list[0].main.humidity + "%";
        LbWind.Text = result.list[0].wind.speed + "km/h";
        imgWeatherIcon.Source = result.list[0].weather[0].fullIconUrl;
    }

    private void CvWeather_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var sectionItems = e.CurrentSelection;

        
    }
}