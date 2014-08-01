using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Kipware.MvvmCross.Plugin.Dialogs;

namespace Dialogs.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            var instance = new DroidDialogService();
            Mvx.RegisterSingleton<IDialogService>(instance);
        }
    }
}