using MasonVeteransMemorial.Controls;
using MasonVeteransMemorial.Pages;
using Xamarin.Forms;

namespace MasonVeteransMemorial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Settings.UseMapPage = false;

            MainPage = new MainAppTabContainer();
        }
    }
}
