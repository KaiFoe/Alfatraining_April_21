using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace PhoneTranslator
{
    public partial class ViewController : UIViewController
    {

        public List<string> telephoneList;
        string translatedNumber = "";

        public ViewController(IntPtr handle) : base(handle)
        {
            telephoneList = new List<string>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            btnTranslate.TouchUpInside += TouchUpInside;
            btnCall.TouchUpInside += TouchUpInside;
        }

        private void TouchUpInside(object sender, EventArgs e)
        {
            
            if (sender == btnTranslate)
            {
                //Übersetze Name in Nummer
                translatedNumber = Phoneword.ToNumber(txtInputName.Text);

                if (translatedNumber == "")
                {
                    btnCall.Enabled = false;
                    btnCall.SetTitle("Keine gueltige Nummer", UIControlState.Normal);
                }
                else
                {
                    btnCall.Enabled = true;
                    txtTranslatedNumber.Text = translatedNumber;
                    btnCall.SetTitle("Rufe " + translatedNumber + " an!", UIControlState.Normal);
                }
                txtInputName.ResignFirstResponder();
            } else if (sender == btnCall)
            {
                var url = new NSUrl("tel:" + translatedNumber);
                //Url-Handler mit dem tel: Praefix nutzen um die TelefonApp zu starten
                if (!UIApplication.SharedApplication.OpenUrl(url))
                {
                    var alert = UIAlertController.Create("Nicht unterstuetzt",
                                                    "Das Telefon wird im Emulator nicht unterstuetzt",
                                                    UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);
                }
                telephoneList.Add(translatedNumber);
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            //Setzen des TableViewController der angezeigt werden soll
            CallHistoryController callHistoryController = segue.DestinationViewController as CallHistoryController;
            if (callHistoryController != null)
                callHistoryController.telephoneList = telephoneList;
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}