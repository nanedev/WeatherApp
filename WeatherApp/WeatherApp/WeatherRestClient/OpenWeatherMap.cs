using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp.WeatherRestClient
{
        public class OpenWeatherMap<T>
        {
            private const string OpenWeatherApi = "http://api.openweathermap.org/data/2.5/weather";
        private const string OpenWeatherApiForecast = "http://api.openweathermap.org/data/2.5/forecast/daily";
        private const string ByCityNameQuery = "?q=";
        private const string Units = "&units=metric";
            private const string Key = "6b707439878c77dcc5db0d485b7a86c4";
        private const string NumberDays = "&cnt=5";
            HttpClient _httpClient = new HttpClient();

            public async Task<T> GetAllWeathers(string city)
            {
                var json = await _httpClient.GetStringAsync(OpenWeatherApi + ByCityNameQuery + city + Units + "&appid=" + Key);
                var getWeatherModels = JsonConvert.DeserializeObject<T>(json);
                return getWeatherModels;
            }

        public async Task<T> GetAllWeathersLocation(double lat, double lon)
        {
            var json = await _httpClient.GetStringAsync(OpenWeatherApi + "?lat=" + lat + "&lon=" + lon + Units + "&appid=" + Key);
            var getWeatherModels = JsonConvert.DeserializeObject<T>(json);
            return getWeatherModels;
        }

        public async Task<T> GetAllWeathersForecast(string city)
        {
            var json = await _httpClient.GetStringAsync(OpenWeatherApiForecast + ByCityNameQuery + city + Units + NumberDays + "&appid=" + Key);
            var getWeatherModels = JsonConvert.DeserializeObject<T>(json);
            return getWeatherModels;
        }

        public async Task<T> GetAllWeathersLocationForecast(double lat, double lon)
        {

            var json = await _httpClient.GetStringAsync(OpenWeatherApiForecast + "?lat=" + lat + "&lon=" + lon + NumberDays + Units + "&appid=" + Key);
         
            var getWeatherModels = JsonConvert.DeserializeObject<T>(json);
            return getWeatherModels;
        }
    }
   
}
