using System;
using System.Collections.Generic;
using MasonVeteransMemorial.Common;
using MasonVeteransMemorial.ViewModels;
using Xamarin.Forms;

namespace MasonVeteransMemorial.Pages
{
    public partial class AboutPage : ContentPage, IAboutViewModelDelegate
    {
        public AboutViewModel ViewModel => BindingContext as AboutViewModel;

        public AboutPage()
        {
            Intitialize();
        }

        protected void Intitialize()
        {
            InitializeComponent();

            BindingContext = new AboutViewModel();
            ViewModel.Delegate = this;

            Title = "About";
            Icon = "info";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadCommand.Execute(null);
        }

        public void OnLoadSuccess()
        {
        }

        public void OnLoadFailure(string title, string message)
        {
        }

        protected void OnLogoClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(Settings.MasonHomePageUrl));
        }
    }
}
