using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Text;
using Android.Widget;
using Cirrious.CrossCore;
using Kipware.MvvmCross.Plugin.Dialogs;
using Cirrious.CrossCore.Droid.Platform;

namespace Dialogs.Droid
{
    public class DroidDialogService : IDialogService
    {
        public string DefaultConfirmText { get; set; }
        public string DefaultCancelText { get; set; }

        public Task AlertAsync(string message, string title, string confirmText = null)
        {
            var tcs = new TaskCompletionSource<object>();
            ExecuteOnMainThread(() =>
            {
                var builder = new AlertDialog.Builder(TopActivity)
                    .SetTitle(title)
                    .SetMessage(message)
                    .SetPositiveButton(confirmText ?? DefaultConfirmText, (s, a) => tcs.TrySetResult(new object()));

                var dialog = builder.Create();
                EventHandler dismissHandler = null;
                dismissHandler = (sender, args) =>
                {
                    dialog.DismissEvent -= dismissHandler;
                    tcs.TrySetResult(new object());
                };
                dialog.DismissEvent += dismissHandler;
                dialog.Show();
            });

            return tcs.Task;
        }

        public Task<bool> ConfirmAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            ExecuteOnMainThread(() =>
            {
                var builder = new AlertDialog.Builder(TopActivity)
                    .SetTitle(title)
                    .SetMessage(message)
                    .SetPositiveButton(confirmText ?? DefaultConfirmText, (sender, args) => tcs.TrySetResult(true))
                    .SetNegativeButton(cancelText ?? DefaultCancelText, (sender, args) => tcs.TrySetResult(false));

                var dialog = builder.Create();
                EventHandler dismissHandler = null;
                dismissHandler = (sender, args) =>
                {
                    dialog.DismissEvent -= dismissHandler;
                    tcs.TrySetResult(false);
                };
                dialog.DismissEvent += dismissHandler;
                dialog.Show();
            });

            return tcs.Task;
        }

        public Task<PromptResult<string>> StringPromptAsync(string message, string title, string defaultText = null, string confirmText = null,
            string cancelText = null)
        {
            var tcs = new TaskCompletionSource<PromptResult<string>>();

            ExecuteOnMainThread(() =>
            {
                var editText = new EditText(TopActivity) {Text = defaultText, InputType = InputTypes.ClassText};
                var builder = new AlertDialog.Builder(TopActivity)
                    .SetTitle(title)
                    .SetMessage(message)
                    .SetView(editText)
                    .SetPositiveButton(confirmText ?? DefaultConfirmText,
                        (sender, args) => tcs.TrySetResult(new PromptResult<string>(true, editText.Text)))
                    .SetNegativeButton(cancelText ?? DefaultCancelText,
                        (sender, args) => tcs.TrySetResult(new PromptResult<string>(false, string.Empty)));

                var dialog = builder.Create();
                EventHandler dismissHandler = null;
                dismissHandler = (sender, args) =>
                {
                    dialog.DismissEvent -= dismissHandler;
                    tcs.TrySetResult(new PromptResult<string>(false, string.Empty));
                };
                dialog.DismissEvent += dismissHandler;
                dialog.Show();
            });

            return tcs.Task;
        }

        public Task<PromptResult<string>> PasswordPromptAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            var tcs = new TaskCompletionSource<PromptResult<string>>();

            ExecuteOnMainThread(() =>
            {
                var editText = new EditText(TopActivity)
                {
                    InputType = InputTypes.ClassText | InputTypes.TextVariationPassword
                };
                var builder = new AlertDialog.Builder(TopActivity)
                    .SetTitle(title)
                    .SetMessage(message)
                    .SetView(editText)
                    .SetPositiveButton(confirmText ?? DefaultConfirmText,
                        (sender, args) => tcs.TrySetResult(new PromptResult<string>(true, editText.Text)))
                    .SetNegativeButton(cancelText ?? DefaultCancelText,
                        (sender, args) => tcs.TrySetResult(new PromptResult<string>(false, string.Empty)));

                var dialog = builder.Create();
                EventHandler dismissHandler = null;
                dismissHandler = (sender, args) =>
                {
                    dialog.DismissEvent -= dismissHandler;
                    tcs.TrySetResult(new PromptResult<string>(false, string.Empty));
                };

                dialog.DismissEvent += dismissHandler;
                dialog.Show();
            });

            return tcs.Task;
        }

        private Toast _toast;
        private Timer _hideTimer;
        public void ShowSubtleNotification(string text, SubtleNotificationDuration duration = SubtleNotificationDuration.Short)
        {
            _toast = new Toast(TopActivity);
            _toast.Duration = duration == SubtleNotificationDuration.Short
                ? ToastLength.Short
                : ToastLength.Long;
            _toast.Show();
        }

        public void HideSubtleNotification()
        {
            if (_toast != null)
                _toast.Cancel();
        }

        private Activity TopActivity
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        private void ExecuteOnMainThread(Action action)
        {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
                action();
            else
                Application.SynchronizationContext.Post(state =>
                {
                    try
                    {
                        action();
                    }
                    catch
                    {
                    }
                }, null);
        }
    }
}
