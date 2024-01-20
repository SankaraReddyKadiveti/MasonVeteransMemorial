using Android.App;
using Android.Views.InputMethods;
using MasonVeteransMemorial.Droid.NativeImplementations;
using MasonVeteransMemorial.NativeImplementations;

[assembly: Xamarin.Forms.Dependency(typeof(DroidKeyboardHelper))]
namespace MasonVeteransMemorial.Droid.NativeImplementations
{
    public class DroidKeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            var context = MainActivity.Instance;

            var inputMethodManager = context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}
