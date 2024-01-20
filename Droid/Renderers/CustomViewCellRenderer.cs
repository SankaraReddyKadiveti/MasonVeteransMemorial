using System;
using Android.Content;
using Android.Views;
using MasonVeteransMemorial.Controls;
using MasonVeteransMemorial.Droid.Renderers.MasonVeteransMemorial.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

/*** For future use - Need to redesign ***/
//[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace MasonVeteransMemorial.Droid.Renderers
{
    namespace MasonVeteransMemorial.iOS.Renderers
    {
        public class CustomViewCellRenderer : Xamarin.Forms.Platform.Android.ViewCellRenderer
        {
            private Android.Views.View _cellCore;
            private bool _selected;

            protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
            {
                //return base.GetCellCore(item, convertView, parent, context);
                _cellCore = base.GetCellCore(item, convertView, parent, context);
                _selected = false;
                return _cellCore;
            }

            protected override void OnCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
            {
                base.OnCellPropertyChanged(sender, args);
                if (args.PropertyName == "IsSelected")
                {
                    _selected = !_selected;
                    var extendedViewCell = sender as CustomViewCell;
                    if (_selected)
                        //_cellCore.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
                        _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                    else
                        _cellCore.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
        }
    }

}
