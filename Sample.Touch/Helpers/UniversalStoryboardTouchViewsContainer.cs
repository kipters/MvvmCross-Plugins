using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Sample.Touch.Helpers
{
    public class UniversalStoryboardTouchViewsContainer : MvxTouchViewsContainer
    {
        private readonly string _storyboardName;

        public UniversalStoryboardTouchViewsContainer(string storyboardName = "MainStoryboard")
        {
            var deviceName = UIDevice.CurrentDevice.Model;
            _storyboardName = string.Format("{0}_{1}", storyboardName, deviceName.StartsWith("iPad")
                ? "iPad"
                : "iPhone");
        }

        protected override IMvxTouchView CreateViewOfType(Type viewType, MvxViewModelRequest request)
        {
            return (IMvxTouchView) UIStoryboard.FromName(_storyboardName, null).InstantiateViewController(viewType.Name);
        }
    }
}