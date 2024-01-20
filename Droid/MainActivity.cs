using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FabricSdk;
using CrashlyticsKit;

namespace MasonVeteransMemorial.Droid
{
    [Activity(Label = "MasonVeteransMemorial.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Instance = this;

            Crashlytics.Instance.Initialize();
            Fabric.Instance.Debug = true;
            Fabric.Instance.Initialize(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
