using Cirrious.CrossCore.Plugins;

namespace Kipware.MvvmCross.Plugin.Dialogs
{
    public class DialogPluginConfiguration : IMvxPluginConfiguration
    {
        public DialogPluginConfiguration(string defaultCancelText, string defaultConfirmText)
        {
            DefaultCancelText = defaultCancelText;
            DefaultConfirmText = defaultConfirmText;
        }

        public string DefaultCancelText { get; set; }
        public string DefaultConfirmText { get; set; }
    }
}
