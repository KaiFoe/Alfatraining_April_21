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

namespace Zitate
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAnzahl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblModus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch swModus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtAnzahl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblAnzahl != null) {
                lblAnzahl.Dispose ();
                lblAnzahl = null;
            }

            if (lblModus != null) {
                lblModus.Dispose ();
                lblModus = null;
            }

            if (swModus != null) {
                swModus.Dispose ();
                swModus = null;
            }

            if (txtAnzahl != null) {
                txtAnzahl.Dispose ();
                txtAnzahl = null;
            }
        }
    }
}