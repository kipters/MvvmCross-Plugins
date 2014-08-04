using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using Kipware.MvvmCross.Plugin.Dialogs;

namespace Sample.Core.ViewModels
{
    public class DialogsViewModel : MvxViewModel
    {
        private readonly IDialogService _dialogService;

        public DialogsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private string _confirmText;
        public string ConfirmText { get { return _confirmText; } set { _confirmText = value; RaisePropertyChanged(() => ConfirmText); } }

        private string _cancelText;
        public string CancelText { get { return _cancelText; } set { _cancelText = value; RaisePropertyChanged(() => CancelText); } }

        private string _message = "message";
        public string Message { get { return _message; } set { _message = value; RaisePropertyChanged(() => Message); } }

        private string _title = "title";
        public string Title { get { return _title; } set { _title = value; RaisePropertyChanged(() => Title); } }

        private ICommand _alertCommand;
        public ICommand AlertCommand
        {
            get
            {
                _alertCommand = _alertCommand ?? new MvxCommand(async () =>
                {
                    await _dialogService.AlertAsync(Message, Title, ConfirmText);
                });
                return _alertCommand;
            }
        }

        private ICommand _confirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                _confirmCommand = _confirmCommand ?? new MvxCommand(async () =>
                {
                    var result = await _dialogService.ConfirmAsync(Message, Title, ConfirmText, CancelText);
                    await _dialogService.AlertAsync(string.Format("Result: {0}", result), "Prompt result");
                });
                return _confirmCommand;
            }
        }

        private ICommand _stringPromptCommand;
        public ICommand StringPromptCommand
        {
            get
            {
                _stringPromptCommand = _stringPromptCommand ?? new MvxCommand(async () =>
                {
                    var result = await _dialogService.StringPromptAsync(Message, Title, "defaultText", ConfirmText, CancelText);
                    await _dialogService.AlertAsync(string.Format("Completed: {0}, Value: {1}", result.Confirmed, result.Result), "Prompt result");
                });
                return _stringPromptCommand;
            }
        }

        private ICommand _passwordPromptCommand;
        public ICommand PasswordPromptCommand
        {
            get
            {
                _passwordPromptCommand = _passwordPromptCommand ?? new MvxCommand(async () =>
                {
                    var result = await _dialogService.PasswordPromptAsync(Message, Title, ConfirmText, CancelText);
                    await _dialogService.AlertAsync(string.Format("Completed: {0}, Value: {1}", result.Confirmed, result.Result), "Prompt result");
                });
                return _passwordPromptCommand;
            }
        }

        private bool _longToast;
        public bool LongToast { get { return _longToast; } set { _longToast = value; RaisePropertyChanged(() => LongToast); } }

        private ICommand _showSubtleNotificationCommand;
        public ICommand ShowSubtleNotificationCommand
        {
            get
            {
                _showSubtleNotificationCommand = _showSubtleNotificationCommand ?? new MvxCommand(() =>
                {
                    _dialogService.ShowSubtleNotification(Message, LongToast ? SubtleNotificationDuration.Long : SubtleNotificationDuration.Short);
                });
                return _showSubtleNotificationCommand;
            }
        }

        private ICommand _hideSubtleNotificationCommand;
        public ICommand HideSubtleNotificationCommand
        {
            get
            {
                _hideSubtleNotificationCommand = _hideSubtleNotificationCommand ?? new MvxCommand(() =>
                {
                    _dialogService.HideSubtleNotification();
                });
                return _hideSubtleNotificationCommand;
            }
        }
    }
}
