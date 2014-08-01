using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Kipware.MvvmCross.Plugin.Dialogs.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            var instance = new TouchDialogService();
            Mvx.RegisterSingleton<IDialogService>(instance);
        }
    }
}