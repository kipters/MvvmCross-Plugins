using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Kipware.MvvmCross.Plugin.Dialogs.WindowsPhone
{
    public class Plugin : IMvxPlugin
    {
        //private DialogPluginConfiguration _config;
        public void Load()
        {
            var instance = new PhoneDialogService();
            Mvx.RegisterSingleton<IDialogService>(instance);
        }
    }
}
