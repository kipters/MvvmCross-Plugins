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
	[Register ("FieldsViewController")]
	partial class FieldsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField CancelTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField ConfirmTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField MessageField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TitleField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CancelTextField != null) {
				CancelTextField.Dispose ();
				CancelTextField = null;
			}
			if (ConfirmTextField != null) {
				ConfirmTextField.Dispose ();
				ConfirmTextField = null;
			}
			if (MessageField != null) {
				MessageField.Dispose ();
				MessageField = null;
			}
			if (TitleField != null) {
				TitleField.Dispose ();
				TitleField = null;
			}
		}
	}
}
