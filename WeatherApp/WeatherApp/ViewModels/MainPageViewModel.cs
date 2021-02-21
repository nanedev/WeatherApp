using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;

        private DelegateCommand _openDetailsPage;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
        }

        public DelegateCommand OpenDetailsPage =>
            _openDetailsPage ?? (_openDetailsPage = new DelegateCommand(ExecuteOpenDetailsPage));


        async void ExecuteOpenDetailsPage()
        {
            await _navigationService.NavigateAsync("DetailsPage");
        }
    }
}
