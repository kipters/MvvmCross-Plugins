using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Sample.Core.ViewModels;

namespace Sample.Touch
{
	partial class DialogsViewController : MvxTableViewController
	{
		public DialogsViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var binding = this.CreateBindingSet<DialogsViewController, DialogsViewModel>();
            binding.Bind(AlertButton).To(x => x.AlertCommand);
            binding.Bind(ConfirmButton).To(x => x.ConfirmCommand);
            binding.Bind(StringButton).To(x => x.StringPromptCommand);
            binding.Bind(PasswordButton).To(x => x.PasswordPromptCommand);
            binding.Bind(lengthSwitch).To(x => x.LongToast);
            binding.Bind(lengthLabel).To(x => x.LongToast).WithConversion("SubtleNotificationLengthToString", null);
            binding.Bind(ShowSubtleButton).To(x => x.ShowSubtleNotificationCommand);
            binding.Bind(HideSubtleButton).To(x => x.HideSubtleNotificationCommand);
            binding.Apply();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "FieldsSegue")
            {
                var navController = segue.DestinationViewController as UINavigationController;
                var destController = navController.ViewControllers[0] as MvxTableViewController;
                destController.DataContext = DataContext;
            }
        }
	}
}
