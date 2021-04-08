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

namespace Notizen
{
    [Register ("NotizListController")]
    partial class NotizListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barBtnItemAdd { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (barBtnItemAdd != null) {
                barBtnItemAdd.Dispose ();
                barBtnItemAdd = null;
            }
        }
    }
}