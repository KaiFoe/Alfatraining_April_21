using Foundation;
using System;
using UIKit;

namespace Zitate
{
    public partial class ViewController : UIViewController
    {
        NSUserDefaults prefList;

        public ViewController(IntPtr handle) : base(handle)
        {
            prefList = NSUserDefaults.StandardUserDefaults;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            txtAnzahl.Text = prefList.StringForKey("Anzahl");
           
                
            swModus.On = prefList.BoolForKey("Modus");



            UIBarButtonItem barBtnSave = new UIBarButtonItem(UIBarButtonSystemItem.Save, (object sender, EventArgs args) =>
            {
                prefList.SetString(txtAnzahl.Text, "Anzahl");
                prefList.SetBool(swModus.On, "Modus");
            });

            NavigationItem.RightBarButtonItem = barBtnSave;
        }
        
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}