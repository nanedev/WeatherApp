using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
   public class WeatherDays
    {
        public List<WeatherItem> List { get; set; }
    }

    public class WeatherItem
    {
        private string _date;
        [JsonProperty("dt")]
        public string Date
        {
            get
            {
                return _date;
            }

            set
            {
                var millis = int.Parse(value);
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
                dtDateTime = dtDateTime.AddMilliseconds(millis).ToLocalTime();
                string formatted = dtDateTime.ToString("dddd, dd");

                var localDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(millis)
        .DateTime.ToLocalTime();

                _date = localDateTimeOffset.ToString("dddd, dd");
            }
        }

        
        public Temperature temp { get; set; }

        public List<WeatherDescription> weather { get; set; }
    }

    public class Temperature
    {
        private string _temperature;
        [JsonProperty("day")]
        public string DayTemp {
            get
            {
                return _temperature;
            }
            set
            {

                var temperature = double.Parse(value);

                _temperature = Convert.ToInt32(temperature).ToString();
            }
        }
    }

    public class WeatherDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        public string _iconImage;
        [JsonProperty("icon")]
        public string Icon {
            get
            {
                return _iconImage;
            }
            set
            {
                _iconImage = "http://openweathermap.org/img/w/" + value + ".png";
                Console.WriteLine("icon url: " + _iconImage);
            }
        }

    }
}
