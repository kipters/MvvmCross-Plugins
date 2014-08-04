// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Sample.Touch
{
	[Register ("DialogsViewController")]
	partial class DialogsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton AlertButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ConfirmButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton HideSubtleButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lengthLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch lengthSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton PasswordButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ShowSubtleButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton StringButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AlertButton != null) {
				AlertButton.Dispose ();
				AlertButton = null;
			}

			if (ConfirmButton != null) {
				ConfirmButton.Dispose ();
				ConfirmButton = null;
			}

			if (PasswordButton != null) {
				PasswordButton.Dispose ();
				PasswordButton = null;
			}

			if (StringButton != null) {
				StringButton.Dispose ();
				StringButton = null;
			}

			if (lengthLabel != null) {
				lengthLabel.Dispose ();
				lengthLabel = null;
			}

			if (lengthSwitch != null) {
				lengthSwitch.Dispose ();
				lengthSwitch = null;
			}

			if (ShowSubtleButton != null) {
				ShowSubtleButton.Dispose ();
				ShowSubtleButton = null;
			}

			if (HideSubtleButton != null) {
				HideSubtleButton.Dispose ();
				HideSubtleButton = null;
			}
		}
	}
}
