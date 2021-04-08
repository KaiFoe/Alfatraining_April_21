using Foundation;
using System;
using UIKit;

namespace WetterApp
{
    public partial class ViewController : UIViewController
    {
        Utility utility;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            utility = new Utility();

            btnSenden.TouchUpInside += BtnSenden_TouchUpInside;
            
                 
        }

        private void BtnSenden_TouchUpInside(object sender, EventArgs e)
        {
            Wetter wetter = utility.jsonGetWetter(txtInputCity.Text);
            lblTemp.Text = "Aktuelle Temperatur: " + wetter.Temp;
            lblLuftdruck.Text = "Aktueller Luftdruck: " + wetter.Pressure;
            lblLuftfeuchtigkeit.Text = "Aktuelle Luftfeuchtigkeit: " + wetter.Humidity;
            lblWindgeschwindigkeit.Text = "Aktuelle Windgeschwindigkeit: " + wetter.Windspeed;
            lblBeschreibung.Text = wetter.Description;
            imgvWetter.Image = wetter.Image;


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}