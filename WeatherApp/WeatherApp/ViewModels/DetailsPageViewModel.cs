using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ServiceHandler;
using Xamarin.Essentials;
using Prism.Services;

namespace WeatherApp.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IPageDialogService PageDialogService;

        private DelegateCommand _saveCityDelegate;

        WeatherServices _weatherServices = new WeatherServices();

        private WeatherMainModel _weatherMainModel;  // for xaml binding
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

        public DetailsPageViewModel(IPageDialogService pageDialogService) : base(pageDialogService)
        {
            PageDialogService = pageDialogService;
            
            var cityPreferences = Preferences.Get("city", "");

            if (!string.IsNullOrEmpty(cityPreferences))
            {
                City = cityPreferences;
            }
        }

        public DelegateCommand SaveCityDelegate =>
            _saveCityDelegate ?? (_saveCityDelegate = new DelegateCommand(ExecuteSaveCityDelegate));


        async void ExecuteSaveCityDelegate()
        {
            Preferences.Set("city", _city);
            await PageDialogService.DisplayAlertAsync("", "City saved for next search", "OK");
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
                });
                OnPropertyChanged();
            }
        }

        private string _iconImageString; // for weather icon image string binding
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


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
