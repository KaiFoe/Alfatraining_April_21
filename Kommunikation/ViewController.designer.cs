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

namespace Kommunikation
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnGivenContact { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnNewContact { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnNewContactWithData { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSendEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSendSMS { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOutput { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnGivenContact != null) {
                btnGivenContact.Dispose ();
                btnGivenContact = null;
            }

            if (btnNewContact != null) {
                btnNewContact.Dispose ();
                btnNewContact = null;
            }

            if (btnNewContactWithData != null) {
                btnNewContactWithData.Dispose ();
                btnNewContactWithData = null;
            }

            if (btnSendEmail != null) {
                btnSendEmail.Dispose ();
                btnSendEmail = null;
            }

            if (btnSendSMS != null) {
                btnSendSMS.Dispose ();
                btnSendSMS = null;
            }

            if (lblOutput != null) {
                lblOutput.Dispose ();
                lblOutput = null;
            }
        }
    }
}