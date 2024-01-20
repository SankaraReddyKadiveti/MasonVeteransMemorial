using System;
using MasonVeteransMemorial.Pages;

using Xamarin.Forms;

namespace MasonVeteransMemorial.Controls
{
    public class MainAppTabContainer : BottomBarPage
    {
        public ContentPage[] Pages = new ContentPage[]
        {
            new MainPage(),
            new SearchPage(),
            new AboutPage(),
            //new SkiaToolsPage()
        };

        public MainAppTabContainer()
        {
            if (Settings.UseMapPage)
                Pages[2] = new MemorialMapPage();

            foreach (var page in Pages)
                Children.Add(page);

            if (Device.RuntimePlatform == Device.iOS)
                BarTextColor = (Color)App.Current.Resources["HeadingTextColor"];
            else
                BarTextColor = Color.Black;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage.Title;
        }
    }
}
