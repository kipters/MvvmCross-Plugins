using System;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Frankdilo.FDStatusBarNotifierView;
using Cirrious.CrossCore;

namespace Kipware.MvvmCross.Plugin.Dialogs.Touch
{
    public class TouchDialogService : IDialogService
    {
        public string DefaultConfirmText { get; set; }
        public string DefaultCancelText { get; set; }

        private UINavigationController _rootController;

        public TouchDialogService()
        {
            var presenterInstance = Mvx.Resolve<IMvxTouchViewPresenter>();
            var presenter = presenterInstance as MvxTouchViewPresenter;
            _rootController = presenter.MasterNavigationController;
            //_rootController = (Mvx.Resolve<IMvxTouchViewPresenter>() as MvxTouchViewPresenter).MasterNavigationController;
        }

        public Task AlertAsync(string message, string title, string confirmText = null)
        {
            var tcs = new TaskCompletionSource<object>();

            var uav = new UIAlertView(title, message, null, confirmText ?? DefaultConfirmText, null);
            EventHandler<UIButtonEventArgs> handler = null;
            handler = (sender, args) =>
            {
                uav.Clicked -= handler;
                tcs.TrySetResult(new object());
            };
            uav.Clicked += handler;
            uav.Show();

            return tcs.Task;
        }

        public Task<bool> ConfirmAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            var uav = new UIAlertView(title, message, null, confirmText ?? DefaultConfirmText, cancelText ?? DefaultCancelText);
            EventHandler<UIButtonEventArgs> handler = null;
            handler = (sender, args) =>
            {
                uav.Clicked -= handler;
                tcs.TrySetResult(args.ButtonIndex == 0);
            };
            uav.Clicked += handler;
            uav.Show();

            return tcs.Task;
        }

        public Task<PromptResult<string>> StringPromptAsync(string message, string title, string defaultText = null, string confirmText = null,
            string cancelText = null)
        {
            var tcs = new TaskCompletionSource<PromptResult<string>>();

            var uav = new UIAlertView(title, message, null, confirmText ?? DefaultConfirmText, cancelText ?? DefaultCancelText)
            {
                AlertViewStyle = UIAlertViewStyle.PlainTextInput
            };
            var textField = uav.GetTextField(0);
            textField.Text = defaultText;

            EventHandler<UIButtonEventArgs> handler = null;
            handler = (sender, args) =>
            {
                uav.Clicked -= handler;
                var result = args.ButtonIndex == 0
                    ? new PromptResult<string>(true, uav.GetTextField(0).Text)
                    : new PromptResult<string>(false, string.Empty);
                tcs.TrySetResult(result);
            };
            uav.Clicked += handler;
            uav.Show();

            return tcs.Task;
        }

        public Task<PromptResult<string>> PasswordPromptAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            var tcs = new TaskCompletionSource<PromptResult<string>>();

            var uav = new UIAlertView(title, message, null, confirmText ?? DefaultConfirmText, cancelText ?? DefaultCancelText)
            {
                AlertViewStyle = UIAlertViewStyle.SecureTextInput
            };

            EventHandler<UIButtonEventArgs> handler = null;
            handler = (sender, args) =>
            {
                uav.Clicked -= handler;
                var result = args.ButtonIndex == 0
                    ? new PromptResult<string>(true, uav.GetTextField(0).Text)
                    : new PromptResult<string>(false, string.Empty);
                tcs.TrySetResult(result);
            };
            uav.Clicked += handler;
            uav.Show();

            return tcs.Task;
        }

        FDStatusBarNotifierView _view;
        public void ShowSubtleNotification(string text, SubtleNotificationDuration duration = SubtleNotificationDuration.Short)
        {
            _view = new FDStatusBarNotifierView(text)
            { 
                ManuallyHide = false, 
                ShouldHideOnTap = true, 
                TimeOnScreen = TimeSpan.FromMilliseconds(duration == SubtleNotificationDuration.Short 
                    ? 2000 
                    : 3500) 
            };
            _view.ShowAboveNavigationController(_rootController);
        }

        public void HideSubtleNotification()
        {
            if (_view == null)
                return;

            _view.Hide();
        }
    }
}