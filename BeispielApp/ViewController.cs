using Foundation;
using System;
using UIKit;

namespace BeispielApp
{
    public partial class ViewController : UIViewController
    {
        
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            //btnExample.TouchUpInside += BtnExample_TouchUpInside;
            btnExample.TouchUpInside += (object sender, EventArgs args) =>
            {
                lblOutput.Text = "Hallo Kai";
            };
        }

        private void BtnExample_TouchUpInside(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}