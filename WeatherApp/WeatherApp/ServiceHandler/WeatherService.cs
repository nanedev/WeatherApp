using System;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.WeatherRestClient;

namespace WeatherApp.ServiceHandler
{
    public class WeatherServices
    {
        OpenWeatherMap<WeatherMainModel> _openWeatherRest = new OpenWeatherMap<WeatherMainModel>();
        public async Task<WeatherMainModel> GetWeatherDetails(string city)
        {
            var getWeatherDetails = await _openWeatherRest.GetAllWeathers(city);
            return getWeatherDetails;
        }

        public async Task<WeatherMainModel> GetWeatherDetailsLocation(double lat, double lon)
        {
            var getWeatherDetails = await _openWeatherRest.GetAllWeathersLocation(lat, lon);
            return getWeatherDetails;
        }
    }
}
