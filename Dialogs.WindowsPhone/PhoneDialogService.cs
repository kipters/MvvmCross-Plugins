using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation.Metadata;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Kipware.MvvmCross.Plugin.Dialogs.WindowsPhone
{
    public class PhoneDialogService : IDialogService
    {
        private readonly Timer _timer;

        public PhoneDialogService()
        {
            SystemTray.ProgressIndicator = new ProgressIndicator()
            {
                IsIndeterminate = false,
                Value = 0,
                IsVisible = false
            };

            _timer = new Timer(state => ExecuteOnMainThread(HideSubtleNotification), null, Timeout.Infinite, Timeout.Infinite);
        }

        public string DefaultConfirmText { get; set; }
        public string DefaultCancelText { get; set; }
        public Task AlertAsync(string message, string title, string confirmText = null)
        {
            return internalAlertAsync(message, title, confirmText ?? DefaultConfirmText);
        }

        public Task<bool> ConfirmAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            return internalAlertAsync(message, title, confirmText ?? DefaultConfirmText, cancelText ?? DefaultCancelText);
        }

        private Task<bool> internalAlertAsync(string message, string title, params string[] buttons)
        {
            var tcs = new TaskCompletionSource<bool>();
            Guide.BeginShowMessageBox(title, message, buttons, 0, MessageBoxIcon.None, result =>
            {
                var button = Guide.EndShowMessageBox(result);
                tcs.SetResult(button.HasValue && button.Value == 0);
            }, null);
            return tcs.Task;
        }

        public Task<PromptResult<string>> StringPromptAsync(string message, string title, string defaultText = null, string confirmText = null,
            string cancelText = null)
        {
            return internalPromptAsync(message, title, defaultText ?? string.Empty, false);
        }

        public Task<PromptResult<string>> PasswordPromptAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            return internalPromptAsync(message, title, null, true);
        }

        public void ShowSubtleNotification(string text, SubtleNotificationDuration duration = SubtleNotificationDuration.Short)
        {
            var pi = SystemTray.ProgressIndicator;
            pi.IsVisible = false;
            pi.Text = text;
            pi.IsIndeterminate = false;
            pi.Value = 0;
            pi.IsVisible = true;

            var dueTime = duration == SubtleNotificationDuration.Short ? 2000 : 3500;
            _timer.Change(dueTime, Timeout.Infinite);
        }

        public void HideSubtleNotification()
        {
            SystemTray.ProgressIndicator.IsVisible = false;
        }

        private Task<PromptResult<string>> internalPromptAsync(string message, string title, string defaultText,
            bool password)
        {
            var tcs = new TaskCompletionSource<PromptResult<string>>();

            Guide.BeginShowKeyboardInput(PlayerIndex.One, title, message, defaultText, result =>
            {
                var str = Guide.EndShowKeyboardInput(result);
                var promptResult = new PromptResult<string>(str != null, str);
                tcs.SetResult(promptResult);
            }, null, password);

            return tcs.Task;
        }

        private void ExecuteOnMainThread(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
