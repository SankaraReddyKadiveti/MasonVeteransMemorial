using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MasonVeteransMemorial.Common;
using MasonVeteransMemorial.Models;
using MasonVeteransMemorial.ViewModels;
using Xamarin.Forms;

namespace MasonVeteransMemorial.Controls
{
    public partial class SimpleSlideShowViewer : ContentView, ISimpleSlideViewerViewModelDelegate
    {
        public static readonly BindableProperty SlideShowSourceProperty = BindableProperty.Create(nameof(SlideShowSource), typeof(SlideShow), typeof(SimpleSlideShowViewer), null);
        public SlideShow SlideShowSource
        {
            get => (SlideShow)GetValue(SlideShowSourceProperty);
            set => SetValue(SlideShowSourceProperty, value);
        }

        public SimpleSlideViewerViewModel ViewModel => BindingContext as SimpleSlideViewerViewModel;

        public SimpleSlideShowViewer()
        {
            InitializeComponent();

            BindingContext = new SimpleSlideViewerViewModel();
            ViewModel.Delegate = this;
            this.SetBinding(SimpleSlideShowViewer.SlideShowSourceProperty, new Binding("CurrentSlideShow", BindingMode.TwoWay));

            OnPlatformFormatting();
        }

        private void OnPlatformFormatting()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    topButtonBar.IsVisible = false;
                    bottomButtonBar.IsVisible = true;
                    break;
                case Device.iOS:
                    topButtonBar.IsVisible = true;
                    bottomButtonBar.IsVisible = false;
                    break;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SlideShowSource))
            {
                ViewModel.CurrentSlideShow = SlideShowSource;
            }
        }

        private void FadeAnimation(bool fadein)
        {
            if (fadein)
            {
                lblSlideText.FadeTo(1, 250, Easing.CubicIn);
                if (!string.IsNullOrEmpty(ViewModel.CurrentSlide.SlideTitle))
                    lblSlideTitle.FadeTo(1, 250, Easing.CubicIn);
                imgSlideImage.FadeTo(1, 250, Easing.CubicIn);
            }
            else
            {
                lblSlideText.FadeTo(0, 100, Easing.CubicIn);
                if (!string.IsNullOrEmpty(ViewModel.CurrentSlide.SlideTitle))
                    lblSlideTitle.FadeTo(0, 100, Easing.CubicIn);
                imgSlideImage.FadeTo(0, 100, Easing.CubicIn);
            }
        }

        public void OnNavigateCompleted()
        {
        }
    }

    public interface ISimpleSlideViewerViewModelDelegate
    {
        void OnNavigateCompleted();
    }

    public class SimpleSlideViewerViewModel : BaseViewModel
    {
        public ISimpleSlideViewerViewModelDelegate Delegate { get; set; }

        public EventHandler SlideChanged;

        private int _numberOfSlides = 0;
        public int NumberOfSlides
        {
            get => _numberOfSlides;
            set => SetProperty(ref _numberOfSlides, value);
        }

        private SlideShowSlide _currentSlide = new SlideShowSlide();
        public SlideShowSlide CurrentSlide
        {
            get => _currentSlide;
            set => SetProperty(ref _currentSlide, value);
        }

        private SlideShow _currentSlideShow = new SlideShow();
        public SlideShow CurrentSlideShow
        {
            get => _currentSlideShow;
            set => SetProperty(ref _currentSlideShow, value);
        }

        private int _currentSlideIndex = 0;
        public int CurrentSlideIndex
        {
            get => _currentSlideIndex;
            set => SetProperty(ref _currentSlideIndex, value);
        }

        private int _currentSlideNumber = 1;
        public int CurrentSlideNumber
        {
            get => _currentSlideNumber;
            set => SetProperty(ref _currentSlideNumber, value);
        }

        public AppCommand NavigateFirstCommand => new AppCommand(NavigateFirst);
        public AppCommand NavigatePreviousCommand => new AppCommand(NavigatePrevious);
        public AppCommand NavigateNextCommand => new AppCommand(NavigateNext);
        public AppCommand NavigateLastCommand => new AppCommand(NavigateLast);

        public SimpleSlideViewerViewModel()
        {
        }

        public SimpleSlideViewerViewModel(SlideShow slideshow)
        {
            CurrentSlideShow = slideshow;
        }

        protected void NavigateFirst()
        {
            CurrentSlideIndex = 0;
            CurrentSlideNumber = CurrentSlideIndex + 1;
            Delegate?.OnNavigateCompleted();
        }

        protected void NavigatePrevious()
        {
            CurrentSlideIndex = CurrentSlideIndex - 1 < 0 ? 0 : CurrentSlideIndex - 1;
            CurrentSlideNumber = CurrentSlideIndex + 1;
            Delegate?.OnNavigateCompleted();
        }

        protected void NavigateNext()
        {
            CurrentSlideIndex = CurrentSlideIndex + 1 > NumberOfSlides - 1 ? NumberOfSlides - 1 : CurrentSlideIndex + 1;
            CurrentSlideNumber = CurrentSlideIndex + 1;
            Delegate?.OnNavigateCompleted();
        }

        protected void NavigateLast()
        {
            CurrentSlideIndex = NumberOfSlides - 1;
            CurrentSlideNumber = CurrentSlideIndex + 1;
            Delegate?.OnNavigateCompleted();
        }

        private void GoToSlide(int slideIndex)
        {
            CurrentSlide = null;
            CurrentSlide = CurrentSlideShow.Slides[slideIndex];
            SlideChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentSlideIndex))
            {
                GoToSlide(CurrentSlideIndex);
            }
            else if (propertyName == nameof(CurrentSlideShow))
            {
                if (null == CurrentSlideShow)
                    return;

                NumberOfSlides = CurrentSlideShow.Slides.Count;
                GoToSlide(0);
            }
        }
    }
}
