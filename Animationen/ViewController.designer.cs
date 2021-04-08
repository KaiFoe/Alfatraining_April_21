// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Animationen
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCustomAnimation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOpenSecondView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOpenThirdView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCustomAnimation != null) {
                btnCustomAnimation.Dispose ();
                btnCustomAnimation = null;
            }

            if (btnOpenSecondView != null) {
                btnOpenSecondView.Dispose ();
                btnOpenSecondView = null;
            }

            if (btnOpenThirdView != null) {
                btnOpenThirdView.Dispose ();
                btnOpenThirdView = null;
            }
        }
    }
}