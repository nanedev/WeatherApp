using System;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.WeatherRestClient;

namespace WeatherApp.ServiceHandler
{
    public class WeatherServices
    {
        OpenWeatherMap<WeatherMainModel> _openWeatherRest = new OpenWeatherMap<WeatherMainModel>();
        OpenWeatherMap<WeatherDays> _openWeatherRestForecast = new OpenWeatherMap<WeatherDays>();
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

        public async Task<WeatherDays> GetWeatherDetailsForecast(string city)
        {
            var getWeatherDetails = await _openWeatherRestForecast.GetAllWeathersForecast(city);
            return getWeatherDetails;
        }

        public async Task<WeatherDays> GetWeatherDetailsLocationForecast(double lat, double lon)
        {
            var getWeatherDetails = await _openWeatherRestForecast.GetAllWeathersLocationForecast(lat, lon);
            return getWeatherDetails;
        }
    }
}
