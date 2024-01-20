using System;
using MasonVeteransMemorial.Controls;
using MasonVeteransMemorial.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace MasonVeteransMemorial.iOS.Renderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as CustomViewCell;
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = view.SelectedBackgroundColor.ToUIColor(),

            };

            //var cell = base.GetCell(item, reusableCell, tv);
            //if (cell != null)
            //cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;


            return cell;
        }

    }
}