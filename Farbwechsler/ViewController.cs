using Foundation;
using System;
using UIKit;

namespace Farbwechsler
{
    public partial class ViewController : UIViewController
    {
        //Variablen zum speichern unserer Farbwerte
        float red, green, blue;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Event "ValueChanged" fuehrt Methode Slider_ValueChanged aus
            sldrRed.ValueChanged += Slider_ValueChanged; // SldrRed_ValueChanged;
            sldrBlue.ValueChanged += Slider_ValueChanged; //SldrBlue_ValueChanged;
            sldrGreen.ValueChanged += Slider_ValueChanged; //SldrGreen_ValueChanged;

            btnCalcColor.TouchUpInside += BtnCalcColor_TouchUpInside;

            //Sobald Eingabe beendet ist, wird die Methode "Textfield_EditingEnd" ausgeuehrt
            txtRed.EditingDidEnd += Textfield_EditingEnd;
            txtGreen.EditingDidEnd += Textfield_EditingEnd;
            txtBlue.EditingDidEnd += Textfield_EditingEnd;

            
        }

        private void Textfield_EditingEnd(object sender, EventArgs e)
        {
            if (sender == txtRed)
            {
                //Umwandeln des Strings in eine float-Zahl und Zuweisung an den Slider
                sldrRed.Value = float.Parse(txtRed.Text);
                red = sldrRed.Value;
            } else if (sender == txtGreen)
            {
                sldrGreen.Value = float.Parse(txtGreen.Text);
                green = sldrGreen.Value;
            } else if (sender == txtBlue)
            {
                sldrBlue.Value = float.Parse(txtBlue.Text);
                blue = sldrBlue.Value;
            }
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            if (sender == sldrRed)
            {
                red = sldrRed.Value;
                lblRed.Text = "Bitte roten Farbanteil angeben (" + (int)red + ")";
                txtRed.Text = red.ToString();
            } else if (sender == sldrGreen)
            {
                green = sldrGreen.Value;
                lblGreen.Text = "Bitte gruenen Farbanteil angeben (" + (int)green + ")";
                txtGreen.Text = green.ToString();
            } else if (sender == sldrBlue)
            {
                blue = sldrBlue.Value;
                lblBlue.Text = "Bitte blauen Farbanteil angeben (" + (int)blue + ")";
                txtBlue.Text = blue.ToString();
            }
            checkFocus();
        }

        private void BtnCalcColor_TouchUpInside(object sender, EventArgs e)
        {
            checkFocus();
            colorChange();
        }

        private void SldrGreen_ValueChanged(object sender, EventArgs e)
        {
            //Der Variablen "green" den Wert des Sliders zuweisen
            green = sldrGreen.Value;
            //aktuellen Wert im Label mit angeben
            lblGreen.Text = "Bitte gruenen Farbanteil angeben (" + (int)green + ")";
        }

        private void SldrBlue_ValueChanged(object sender, EventArgs e)
        {
            //Der Variablen "blue" den Wert des Sliders zuweisen
            blue = sldrBlue.Value;
            //aktuellen Wert im Label mit angeben
            lblBlue.Text = "Bitte blauen Farbanteil angeben (" + (int)blue + ")";
        }

        private void SldrRed_ValueChanged(object sender, EventArgs e)
        {
            //Der Variablen "red" den Wert des Sliders zuweisen
            red = sldrRed.Value;
            //aktuellen Wert im Label mit angeben
            lblRed.Text = "Bitte roten Farbanteil angeben (" + (int)red + ")";
        }

        //Pruefe ob Textfeld den Focus haelt
        private void checkFocus()
        {
            //pruefen ob TextFeld als erstes antwortet
            txtRed.ResignFirstResponder();
            txtGreen.ResignFirstResponder();
            txtBlue.ResignFirstResponder();
        }

        //Wechsel der Farbe anhand der aktuellen Werte von red, green, blue
        //Wandel und Ausgabe der Farbwerte in Hex-Code
        private void colorChange()
        {
            //Umwandeln von Farbwerten in String mit hexadezimaler Schreibweise
            //X2 = Hexadezimal in Großbuchstaben mit 2 Stellen
            string hexRed = ((int)red).ToString("X2");
            string hexGreen = ((int)green).ToString("X2");
            string hexBlue = ((int)blue).ToString("X2");

            lblOutput.Text = "Farbcode: #" + hexRed + " " + hexGreen + " " + hexBlue;
            //Hintergrundfarbe des Labels setzen
            lblOutput.BackgroundColor = UIColor.FromRGB(red / 255, green / 255, blue / 255);
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}