using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherApp.Converters
{
    public class BackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "";

            int temp = int.Parse((string)value);

            if (temp >= 0 && temp <=10)
            {
                color = "#0000FF";
            }
            else if (temp > 10 && temp <= 15)
            {
                color = "#FFFF00";
            }
            else if (temp > 15 && temp <= 25)
            {
                color = "#FFA500";
            }
            else if (temp > 25)
            {
                color = "#FF0000";
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
