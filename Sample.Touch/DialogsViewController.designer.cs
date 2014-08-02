// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Sample.Touch
{
	[Register ("DialogsViewController")]
	partial class DialogsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton AlertButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ConfirmButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton PasswordButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton StringButton { get; set; }

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
		}
	}
}
