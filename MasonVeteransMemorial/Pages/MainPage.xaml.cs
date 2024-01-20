using System;
using System.Collections.Generic;
using MasonVeteransMemorial.Common;
using MasonVeteransMemorial.ViewModels;
using Xamarin.Forms;

namespace MasonVeteransMemorial.Pages
{
    public partial class MainPage : ContentPage, IMainViewModelViewModelDelegate
    {
        public MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            Initialize();
        }

        protected void Initialize()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
            ViewModel.Delegate = this;

            Title = "Memorial";
            Icon = "ic_account_balance";

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (sender, e) =>
            {
                Device.OpenUri(new Uri("https://www.google.com/maps/?q=39.353381,-84.3083203"));
            };

            slAddress.GestureRecognizers.Add(tapGesture);
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

        protected void OnDonateButtonClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(Settings.MasonDonatePageUrl));
        }
        protected void On3DTourButtonClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(Settings.Mason3DTourUrl));
        }
    }
}
