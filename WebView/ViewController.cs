using Foundation;
using SafariServices;
using System;
using UIKit;

namespace WebView
{
    public partial class ViewController : UIViewController
    {
        //Url fuer spaeteren Aufruf
        NSUrl url = new NSUrl("https://www.google.de");

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Daten von Url holen
            NSUrlRequest request = new NSUrlRequest(url);
            //Daten in WebKitWebView anzeigen
            //webView.LoadRequest(request);

            btnWebView.TouchUpInside += TouchUpInside;
            btnSafari.TouchUpInside += TouchUpInside;
        }

        private void TouchUpInside(object sender, EventArgs e)
        {
            if (sender == btnWebView)
            {
                //SafariWebView instanzieren
                var sfViewController = new SFSafariViewController(url);
                PresentViewController(sfViewController, true, null);
            }
            else if (sender == btnSafari)
            {
                //Safari oeffnen
                UIApplication.SharedApplication.OpenUrl(url);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}