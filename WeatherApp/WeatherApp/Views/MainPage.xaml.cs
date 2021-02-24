
using WeatherApp.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherApp.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = ((MainPageViewModel)this.BindingContext);
        }
    }
}
