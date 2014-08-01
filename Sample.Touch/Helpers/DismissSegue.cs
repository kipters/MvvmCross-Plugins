using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Sample.Touch.Helpers
{
    [Register("DismissSegue")]
    public class DismissSegue : UIStoryboardSegue
    {
        public DismissSegue(IntPtr handle) : base(handle)
        {
        }

        public override void Perform()
        {
            base.Perform();
            SourceViewController.PresentingViewController.DismissViewController(true, null);
        }
    }
}