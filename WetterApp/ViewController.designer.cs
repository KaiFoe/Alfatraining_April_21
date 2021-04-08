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

namespace WetterApp
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSenden { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgvWetter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBeschreibung { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblLuftdruck { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblLuftfeuchtigkeit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTemp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWindgeschwindigkeit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtInputCity { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnSenden != null) {
                btnSenden.Dispose ();
                btnSenden = null;
            }

            if (imgvWetter != null) {
                imgvWetter.Dispose ();
                imgvWetter = null;
            }

            if (lblBeschreibung != null) {
                lblBeschreibung.Dispose ();
                lblBeschreibung = null;
            }

            if (lblLuftdruck != null) {
                lblLuftdruck.Dispose ();
                lblLuftdruck = null;
            }

            if (lblLuftfeuchtigkeit != null) {
                lblLuftfeuchtigkeit.Dispose ();
                lblLuftfeuchtigkeit = null;
            }

            if (lblTemp != null) {
                lblTemp.Dispose ();
                lblTemp = null;
            }

            if (lblWindgeschwindigkeit != null) {
                lblWindgeschwindigkeit.Dispose ();
                lblWindgeschwindigkeit = null;
            }

            if (txtInputCity != null) {
                txtInputCity.Dispose ();
                txtInputCity = null;
            }
        }
    }
}