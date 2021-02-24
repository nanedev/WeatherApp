using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ServiceHandler;
using Xamarin.Essentials;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase,INotifyPropertyChanged
    {
        INavigationService _navigationService;
        CancellationTokenSource cts;


        private DelegateCommand _openDetailsPage;

        public event PropertyChangedEventHandler PropertyChanged;

        WeatherServices _weatherServices = new WeatherServices();
        private WeatherMainModel _weatherMainModel;

        private WeatherDays _weatherDays;

        private double latitude, longitude;

        int _rowHeigth;
        public int RowHeigth
        {
            set
            {
                if (_rowHeigth != value)
                {
                    _rowHeigth = value;
                    OnPropertyChanged();
                    // SetNewColor();
                }
            }
            get
            {
                return _rowHeigth;
            }
        }


        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;

            GetCurrentLocation();
      

        }

        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    latitude = location.Latitude;
                    longitude = location.Longitude;

                    await InitializeGetWeatherAsyncLocation();
                    await InitializeGetWeatherAsyncLocationForecast();
                  
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

      
        public DelegateCommand OpenDetailsPage => 
            _openDetailsPage ?? (_openDetailsPage = new DelegateCommand(ExecuteOpenDetailsPage));


        async void ExecuteOpenDetailsPage()
        {
            await _navigationService.NavigateAsync("DetailsPage");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WeatherMainModel WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                IconImageString = "http://openweathermap.org/img/w/" + _weatherMainModel.weather[0].icon + ".png"; // fetch weather icon image
                OnPropertyChanged();
            }
        }

        public WeatherDays WeatherDays
        {
            get { return _weatherDays; }
            set
            {
                _weatherDays = value;
               // IconImageString = "http://openweathermap.org/img/w/" + _weatherMainModel.weather[0].icon + ".png"; // fetch weather icon image
                OnPropertyChanged();
            }
        }

        private string _city;   // for entry binding and for method parameter value
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                Task.Run(async () => {
                    await InitializeGetWeatherAsync();
                    await InitializeGetWeatherAsyncForecast();
                });
                OnPropertyChanged();
            }
        }

        private string _iconImageString;
        public string IconImageString
        {
            get { return _iconImageString; }
            set
            {
                _iconImageString = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;   // for showing loader when the task is initializing
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true; // set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
                WeatherMainModel = await _weatherServices.GetWeatherDetails(_city);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeGetWeatherAsyncLocation()
        {
            try
            {
                IsBusy = true; // set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
                WeatherMainModel = await _weatherServices.GetWeatherDetailsLocation(latitude, longitude);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeGetWeatherAsyncForecast()
        {
            try
            {
                IsBusy = true; // set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
                WeatherDays = await _weatherServices.GetWeatherDetailsForecast(_city);
                RowHeigth = WeatherDays.List.Count * 10 + WeatherDays.List.Count * 2 * 5;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeGetWeatherAsyncLocationForecast()
        {
            try
            {
                IsBusy = true; // set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
                WeatherDays = await _weatherServices.GetWeatherDetailsLocationForecast(latitude, longitude);
                RowHeigth = WeatherDays.List.Count * 50 + WeatherDays.List.Count * 2 * 5 + 20;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}
