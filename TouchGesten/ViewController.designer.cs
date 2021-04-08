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

namespace TouchGesten
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgvDoubleTap { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgVDragMe { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgvTouch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblStatus { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgvDoubleTap != null) {
                imgvDoubleTap.Dispose ();
                imgvDoubleTap = null;
            }

            if (imgVDragMe != null) {
                imgVDragMe.Dispose ();
                imgVDragMe = null;
            }

            if (imgvTouch != null) {
                imgvTouch.Dispose ();
                imgvTouch = null;
            }

            if (lblStatus != null) {
                lblStatus.Dispose ();
                lblStatus = null;
            }
        }
    }
}