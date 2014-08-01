using System.Threading.Tasks;

namespace Kipware.MvvmCross.Plugin.Dialogs
{
    public interface IDialogService
    {
        string DefaultConfirmText { get; set; }
        string DefaultCancelText { get; set; }

        Task AlertAsync(string message, string title, string confirmText = null);
        Task<bool> ConfirmAsync(string message, string title, string confirmText = null, string cancelText = null);
        Task<PromptResult<string>> StringPromptAsync(string message, string title, string defaultText = null, string confirmText = null, string cancelText = null);

        Task<PromptResult<string>> PasswordPromptAsync(string message, string title, string confirmText = null, string cancelText = null);
    }

    public class PromptResult<T>
    {
        public PromptResult(bool confirmed, T result)
        {
            Confirmed = confirmed;
            Result = result;
        }

        public bool Confirmed { get; set; }
        public T Result { get; set; }
    }
}
