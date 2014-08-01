using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Kipware.MvvmCross.Plugin.Dialogs
{
    public class PluginLoader : IMvxConfigurablePluginLoader
    {
        public static readonly PluginLoader Instance = new PluginLoader();
        private DialogPluginConfiguration _config;

        public void EnsureLoaded()
        {
            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();

            var instance = Mvx.Resolve<IDialogService>();
            instance.DefaultCancelText = _config.DefaultCancelText;
            instance.DefaultConfirmText = _config.DefaultConfirmText;
        }

        public void Configure(IMvxPluginConfiguration configuration)
        {
            _config = (configuration as DialogPluginConfiguration) ?? new DialogPluginConfiguration("cancel", "ok");
        }
    }
}
