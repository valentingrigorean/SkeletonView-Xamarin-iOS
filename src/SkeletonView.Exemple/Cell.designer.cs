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
	[Register ("Cell")]
	partial class Cell
	{
		[Outlet]
		UIKit.UIImageView avatarImage { get; set; }

		[Outlet]
		UIKit.UILabel label1 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (avatarImage != null) {
				avatarImage.Dispose ();
				avatarImage = null;
			}

			if (label1 != null) {
				label1.Dispose ();
				label1 = null;
			}
		}
	}
}
