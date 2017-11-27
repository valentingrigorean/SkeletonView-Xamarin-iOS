// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SkeletonView.Exemple
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		SkeletonView.Views.SKImageView avatarImage { get; set; }

		[Outlet]
		UIKit.UIButton button { get; set; }

		[Outlet]
		UIKit.UIView colorSelectedView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl skeletonTypeSelector { get; set; }

		[Outlet]
		UIKit.UISwitch switchAnimated { get; set; }

		[Outlet]
		UIKit.UITableView tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (avatarImage != null) {
				avatarImage.Dispose ();
				avatarImage = null;
			}

			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}

			if (skeletonTypeSelector != null) {
				skeletonTypeSelector.Dispose ();
				skeletonTypeSelector = null;
			}

			if (switchAnimated != null) {
				switchAnimated.Dispose ();
				switchAnimated = null;
			}

			if (colorSelectedView != null) {
				colorSelectedView.Dispose ();
				colorSelectedView = null;
			}

			if (button != null) {
				button.Dispose ();
				button = null;
			}
		}
	}
}
