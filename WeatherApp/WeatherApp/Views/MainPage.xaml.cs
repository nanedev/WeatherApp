
using WeatherApp.ViewModels;

namespace WeatherApp.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel(null);
        }
    }
}
