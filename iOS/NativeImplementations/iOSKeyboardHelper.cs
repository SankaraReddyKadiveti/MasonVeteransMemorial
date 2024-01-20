using System;
using MasonVeteransMemorial.iOS.NativeImplementations;
using MasonVeteransMemorial.NativeImplementations;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSKeyboardHelper))]
namespace MasonVeteransMemorial.iOS.NativeImplementations
{
    public class iOSKeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}
