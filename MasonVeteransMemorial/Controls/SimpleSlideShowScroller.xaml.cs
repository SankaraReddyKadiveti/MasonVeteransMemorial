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
    public partial class SimpleSlideShowScroller : ContentView, ISimpleSlideScollerViewModelDelegate
    {
        public static readonly BindableProperty SlideShowSourceProperty = BindableProperty.Create(nameof(SlideShowSource), typeof(SlideShow), typeof(SimpleSlideShowScroller), null);
        public SlideShow SlideShowSource
        {
            get => (SlideShow)GetValue(SlideShowSourceProperty);
            set => SetValue(SlideShowSourceProperty, value);
        }

        public static readonly BindableProperty IntroActionProperty = BindableProperty.Create(nameof(IntroAction), typeof(Action<Layout>), typeof(SimpleSlideShowScroller), null);
        public Action<Layout> IntroAction
        {
            get => (Action<Layout>)GetValue(IntroActionProperty);
            set => SetValue(IntroActionProperty, value);
        }

        public static readonly BindableProperty ReadMoreActionProperty = BindableProperty.Create(nameof(ReadMoreAction), typeof(Action), typeof(SimpleSlideShowScroller), null);
        public Action ReadMoreAction
        {
            get => (Action)GetValue(ReadMoreActionProperty);
            set => SetValue(ReadMoreActionProperty, value);
        }

        public SimpleSlideScollerViewModel ViewModel => BindingContext as SimpleSlideScollerViewModel;

        public SimpleSlideShowScroller()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeComponent();

            BindingContext = new SimpleSlideScollerViewModel();
            ViewModel.Delegate = this;
            this.SetBinding(SimpleSlideShowViewer.SlideShowSourceProperty, new Binding("CurrentSlideShow", BindingMode.TwoWay));

            ViewModel.Slide1 = slSlide1;
            ViewModel.ReadMoreSlides = slReadMoreSlides;

            OnPlatformFormatting();
        }

        private void OnPlatformFormatting()
        {

        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SlideShowSource))
            {
                ViewModel.CurrentSlideShow = SlideShowSource;
            }
            else if (propertyName == nameof(IntroAction))
            {
                ViewModel.IntroAction = IntroAction;
            }
            else if (propertyName == nameof(ReadMoreAction))
            {
                ViewModel.ReadMoreAction = ReadMoreAction;
            }
        }

        private void FadeAnimation(bool fadein)
        {

        }

        public void OnNavigateCompleted()
        {
        }
    }

    public interface ISimpleSlideScollerViewModelDelegate
    {
        void OnNavigateCompleted();
    }

    public class SimpleSlideScollerViewModel : BaseViewModel
    {
        public ISimpleSlideScollerViewModelDelegate Delegate { get; set; }

        public EventHandler SlideChanged;

        private bool _readMoreIsVisible;
        public bool ReadMoreIsVisible
        {
            get => _readMoreIsVisible;
            set => SetProperty(ref _readMoreIsVisible, value);
        }

        private StackLayout _slide1;
        public StackLayout Slide1
        {
            get => _slide1;
            set => SetProperty(ref _slide1, value);
        }

        private StackLayout _readMoreSlides;
        public StackLayout ReadMoreSlides
        {
            get => _readMoreSlides;
            set => SetProperty(ref _readMoreSlides, value);
        }

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

        private Action<Layout> _introAction = null;
        public Action<Layout> IntroAction
        {
            get => _introAction;
            set => SetProperty(ref _introAction, value);
        }

        private Action _readMoreAction = null;
        public Action ReadMoreAction
        {
            get => _readMoreAction;
            set => SetProperty(ref _readMoreAction, value);
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

        public AppCommand ReadMoreCommand => new AppCommand(ReadMore);

        public SimpleSlideScollerViewModel()
        {
            Initialize();
        }

        public SimpleSlideScollerViewModel(SlideShow slideshow)
        {
            CurrentSlideShow = slideshow;
            Initialize();
        }

        private void Initialize()
        {
            ReadMoreIsVisible = false;
        }

        private void ReadMore()
        {
            ReadMoreIsVisible = true;
            ReadMoreAction?.Invoke();
        }

        private void BuildSlides()
        {
            if (null == CurrentSlideShow)
                return;

            Slide1.Children.Clear();

            var image = new Image
            {
                Aspect = Aspect.AspectFill,
                Source = CurrentSlideShow.Slides[0].SlideImageContent,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //HeightRequest = 200
            };

            var labelTitle = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                LineBreakMode = LineBreakMode.WordWrap,
                FontAttributes = FontAttributes.Bold,
                Text = CurrentSlideShow.Slides[0].SlideTitle,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0),
            };

            labelTitle.Style = (Style)App.Current.Resources["TitleLabelStyle"];

            var labelContent = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                LineBreakMode = LineBreakMode.WordWrap,
                Text = CurrentSlideShow.Slides[0].SlideTextContent,
                Margin = new Thickness(0, 0, 0, 0),
                FontSize = Device.RuntimePlatform == Device.Android ? 14 : 15,
                HorizontalTextAlignment = CurrentSlideShow.FirstPageImplementsReadMore ? TextAlignment.Center : TextAlignment.Start

            };

            var layoutText = new StackLayout() { Padding = new Thickness(20, 15, 20, 20) };
            if (!string.IsNullOrEmpty(CurrentSlideShow.Slides[0].SlideTitle))
                layoutText.Children.Add(labelTitle);
            layoutText.Children.Add(labelContent);

            if (!CurrentSlideShow.Slides[0].HideImage)
                Slide1.Children.Add(image);
            Slide1.Children.Add(layoutText);

            IntroAction?.Invoke(Slide1);

            ReadMoreSlides.Children.Clear();
            for (var i = 1; i < CurrentSlideShow.Slides.Count; i++)
            {
                var childLayout = new StackLayout() { Spacing = 20 };

                var imageReadMore = new Image
                {
                    Aspect = Aspect.AspectFit,
                    VerticalOptions = LayoutOptions.Start,
                    Source = CurrentSlideShow.Slides[i].SlideImageContent,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 200
                };

                var labelTitleReadMore = new Label
                {
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    LineBreakMode = LineBreakMode.WordWrap,
                    FontAttributes = FontAttributes.Bold,
                    Text = CurrentSlideShow.Slides[i].SlideTitle,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                var labelContentReadMore = new Label
                {
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    LineBreakMode = LineBreakMode.WordWrap,
                    Text = CurrentSlideShow.Slides[i].SlideTextContent,
                    FontSize = Device.RuntimePlatform == Device.Android ? 14 : 15,
                    HorizontalTextAlignment = CurrentSlideShow.FirstPageImplementsReadMore ? TextAlignment.Center : TextAlignment.Start
                };

                var layoutTextReadMore = new StackLayout() { Padding = new Thickness(20, 0, 20, 0) };
                if (!string.IsNullOrEmpty(CurrentSlideShow.Slides[i].SlideTitle))
                    layoutTextReadMore.Children.Add(labelTitleReadMore);
                layoutTextReadMore.Children.Add(labelContentReadMore);

                if (!CurrentSlideShow.Slides[i].HideImage)
                    childLayout.Children.Add(imageReadMore);
                childLayout.Children.Add(layoutTextReadMore);

                ReadMoreSlides.Children.Add(childLayout);
            }

        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentSlideShow))
            {
                if (null == CurrentSlideShow)
                    return;

                ReadMoreIsVisible = !CurrentSlideShow.FirstPageImplementsReadMore;
                CurrentSlideShow.Slides = new System.Collections.ObjectModel.ObservableCollection<SlideShowSlide>(CurrentSlideShow.Slides.Where(x => !x.Skip).ToList());

                BuildSlides();
            }
        }
    }
}
