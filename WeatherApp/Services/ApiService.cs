using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public static class ApiService
    {
        public static async Task<Root> getWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=37b930f7c5714a5d427fc65d2cebbc0b", latitude, longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root> getWeatherByCity(String cityName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=37b930f7c5714a5d427fc65d2cebbc0b", cityName));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
