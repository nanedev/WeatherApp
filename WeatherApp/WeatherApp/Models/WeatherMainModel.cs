using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApp.Models
{
   
        public class WeatherMainModel
        {
            [JsonProperty("name")]
            public string name { get; set; }
            public WeatherTempDetails main { get; set; }
            public List<WeatherSubDetails> weather { get; set; }
            public WeatherWindDetails wind { get; set; }
            public WeatherSysDetails sys { get; set; }
        }

        public class WeatherSubDetails
        {
            [JsonProperty("main")]
            public string main { get; set; }
            [JsonProperty("description")]
            public string description { get; set; }
            [JsonProperty("icon")]
            public string icon { get; set; }

        
        }

        public class WeatherSysDetails
        {
            [JsonProperty("country")]
            public string country { get; set; }
        }

        public class WeatherTempDetails
        {
        private string _temperature;
        [JsonProperty("temp")]
        public string temp
        {
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
            [JsonProperty("humidity")]
            public string humidity { get; set; }

            [JsonProperty("pressure")]
            public string pressure { get; set; }



    }

        public class WeatherWindDetails
        {
            [JsonProperty("speed")]
            public string speed { get; set; }
        }






}
