using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Kipware.MvvmCross.Plugin.Dialogs.WindowsPhone
{
    public class PhoneDialogService : IDialogService
    {
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
            return internalPromptAsynx(message, title, defaultText ?? string.Empty, false);
        }

        public Task<PromptResult<string>> PasswordPromptAsync(string message, string title, string confirmText = null, string cancelText = null)
        {
            return internalPromptAsynx(message, title, null, true);
        }

        private Task<PromptResult<string>> internalPromptAsynx(string message, string title, string defaultText,
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
    }
}
