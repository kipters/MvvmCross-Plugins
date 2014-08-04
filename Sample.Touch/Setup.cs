using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using Sample.Touch.Helpers;

namespace Sample.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

        public Setup(MvxApplicationDelegate applicationDelegate, MvxTouchViewPresenter presenter) : base(applicationDelegate, presenter) { }

        protected override Cirrious.CrossCore.Plugins.IMvxPluginConfiguration GetPluginConfiguration(System.Type plugin)
        {
            if (plugin == typeof(Kipware.MvvmCross.Plugin.Dialogs.PluginLoader))
            {
                var config = new Kipware.MvvmCross.Plugin.Dialogs.DialogPluginConfiguration("cancel", "ok");
                return config;
            }
            return null;
        }

	    protected override IMvxTouchViewsContainer CreateTouchViewsContainer()
	    {
	        return new UniversalStoryboardTouchViewsContainer();
	    }

	    protected override IMvxApplication CreateApp ()
		{
			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
	}
}