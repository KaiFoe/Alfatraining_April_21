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

namespace WebView
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSafari { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnWebView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        WebKit.WKWebView webView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnSafari != null) {
                btnSafari.Dispose ();
                btnSafari = null;
            }

            if (btnWebView != null) {
                btnWebView.Dispose ();
                btnWebView = null;
            }

            if (webView != null) {
                webView.Dispose ();
                webView = null;
            }
        }
    }
}