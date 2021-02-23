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
        [JsonProperty("dt")]
        public string Date { get; set; }

        [JsonProperty("temp")]
        Temp Temprature { get; set; }

        //[JsonProperty("weather")]
        //Weather weather { get; set; }
    }

    public class Temp
    {
        [JsonProperty("day")]
        public string DayTemp { get; set; }
    }

    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
