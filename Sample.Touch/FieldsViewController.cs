using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Sample.Core.ViewModels;

namespace Sample.Touch
{
	partial class FieldsViewController : MvxTableViewController
	{
		public FieldsViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var tapRecognizer = new UITapGestureRecognizer(state => UIApplication.SharedApplication.KeyWindow.EndEditing(true));
            var nc = NSNotificationCenter.DefaultCenter;
            nc.AddObserver(UIKeyboard.WillShowNotification, n => View.AddGestureRecognizer(tapRecognizer));
            nc.AddObserver(UIKeyboard.WillHideNotification, n => View.RemoveGestureRecognizer(tapRecognizer));

            var binding = this.CreateBindingSet<FieldsViewController, DialogsViewModel>();
            binding.Bind(MessageField).To(x => x.Message);
            binding.Bind(TitleField).To(x => x.Title);
            binding.Bind(ConfirmTextField).To(x => x.ConfirmText);
            binding.Bind(CancelTextField).To(x => x.CancelText);
            binding.Apply();
        }
	}
}
