using System;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.CrossCore;
using Frankdilo.FDStatusBarNotifierView;
using System.Threading;
using MonoTouch.Foundation;

namespace Kipware.MvvmCross.Plugin.Dialogs.Touch
{
    public class TouchDialogService : IDialogService
    {
        public string DefaultConfirmText { get; set; }
        public string DefaultCancelText { get; set; }

        private UINavigationController _rootController;

        private UINavigationController RootController
        {
            get
            {
                if (_rootController == null)
                {
                    var presenterInstance = Mvx.Resolve<IMvxTouchViewPresenter>();
                    var presenter = presenterInstance as MvxTouchViewPresenter;
                    _rootController = presenter.MasterNavigationController;
                }

                return _rootController;
            }
        }

        private Timer _hideTimer;
        public TouchDialogService()
        {
            _hideTimer = new Timer(state => ExecuteOnMainThread(HideSubtleNotification), null, Timeout.Infinite, Timeout.Infinite);
            _view = new FDStatusBarNotifierView(string.Empty)
                { 
                    ManuallyHide = true, 
                    //ShouldHideOnTap = true, 
                    /*TimeOnScreen = TimeSpan.FromMilliseconds(duration == SubtleNotificationDuration.Short 
                    ? 2000 
                    : */
                };
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
            _view.Message = text;

            var dueTime = duration == SubtleNotificationDuration.Short ? 2000 : 3500;
            _hideTimer.Change(dueTime, Timeout.Infinite);

            if (_view.IsHidden)
                _view.ShowAboveNavigationController(RootController);
        }

        public void HideSubtleNotification()
        {
            if (_view == null || _view.IsHidden)
                return;

            _view.Hide();
        }

        public void ExecuteOnMainThread(Action action)
        {
            var obj = new NSObject();
            obj.InvokeOnMainThread(new NSAction(action));
        }
    }
}