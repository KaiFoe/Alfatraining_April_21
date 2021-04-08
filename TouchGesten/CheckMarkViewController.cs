using Foundation;
using System;
using UIKit;

namespace TouchGesten
{
    public partial class CheckMarkViewController : UIViewController
    {

        private bool isChecked = false;
        private CheckMarkGestureRecognizer checkMarkGestureRecognizer;

        public CheckMarkViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            WireUpCheckMarkGestureRecognizer();
        }

        private void WireUpCheckMarkGestureRecognizer()
        {
            checkMarkGestureRecognizer = new CheckMarkGestureRecognizer();

            //Wir f|gen unserem CheckMarkGestureRecognizer ein Ziel hinzu
            checkMarkGestureRecognizer.AddTarget(() =>
            {
                if (checkMarkGestureRecognizer.State == (UIGestureRecognizerState.Recognized | UIGestureRecognizerState.Ended))
                {
                    isChecked = !isChecked;
                    if (isChecked)
                    {
                        imgVCheckMe.Image = UIImage.FromBundle("CheckBox_Checked.png");
                    }
                    else
                        imgVCheckMe.Image = UIImage.FromBundle("CheckBox_Unchecked.png");
                }
            });

            View.AddGestureRecognizer(checkMarkGestureRecognizer);
        }
    }
}