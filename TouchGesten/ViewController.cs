using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace TouchGesten
{
    public partial class ViewController : UIViewController
    {
        #region Variablendeklaration
        //Deklaration unserer Variablen
        private bool imageHighlighted = false;
        private bool touchStartedInside = false;
        #endregion

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        #region Touch-Methoden
        //TouchEvent wird gestartet
        //Behandlung von SingleTouch, MultiTouch
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            //Wenn MultiToch aktiviert ist, geben wir die Anzahl der Finger zurueck
            lblStatus.Text = string.Format("Anzahl Finger: {0}", touches.Count);

            //TouchObjekt fuer jedes Objekt in "touches"
            //notwendig, damit wir pruefen koennen ob mndestens ein Finger
            //unsere ImageViews beruehrt
            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                //Pruefen ob eines der Bilder getouched wurde
                //wurde innerhalb der Flaeche des imgVTouch getouched?
                if (imgvTouch.Frame.Contains(touch.LocationInView(View)))
                {
                    imgvTouch.Image = UIImage.FromBundle("TouchMe_Touched.png");
                    lblStatus.Text = "Touches began";
                }

                //Pruefen ob DoubleTap auf der imbvDoubleTap ausgefuehrt wurde
                else if (touch.TapCount == 1 && imgvDoubleTap.Frame.Contains(touch.LocationInView(View)))
                {
                    //aendern der Highlighted-Variablen
                    imageHighlighted = !imageHighlighted;

                    //Wenn imageHighlited = true, dann setze Bild auf "highlighted"
                    if (imageHighlighted)
                    {
                        imgvDoubleTap.Image = UIImage.FromBundle("DoubleTapMe_Highlighted.png");
                        lblStatus.Text = "Double Tap on";
                    }
                    //ansonsten setze Bild wieder auf Ausgangsbild
                    else
                    {
                        imgvDoubleTap.Image = UIImage.FromBundle("DoubleTapMe.png");
                        lblStatus.Text = "Double Tap off";
                    }
                }

                //touch.LocationInView(View) gibt Position des Touch-Events auf der View zurueck
                //imgVDragMe.Frame.Contains prueft ob der uebergebene Punkt innerhalb der imageView liegt
                else if (imgVDragMe.Frame.Contains(touch.LocationInView(View)))
                {
                    touchStartedInside = true;
                }
            }
        }

        //TouchEvent wird geplant (durch den Nutzer) beendet
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;

            if (touch != null)
            {
                if (imgvTouch.Frame.Contains(touch.LocationInView(View)))
                {
                    imgvTouch.Image = UIImage.FromBundle("CheckBox_Start.png");
                    lblStatus.Text = "Touch ended";
                }

                //Zuruecksetzen unserer Hilfsvariablen fuer das DragObjekt
                touchStartedInside = false;
            }
        }

        //Waehrend der/die Finger auf dem Display touched bewegen wir den/die Finger
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                if (imgVDragMe.Frame.Contains(touch.LocationInView(View)))
                {
                    lblStatus.Text = "Touch bewegt";

                    nfloat newPositionX = touch.LocationInView(View).X;
                    nfloat newPositionY = touch.LocationInView(View).Y;

                    nfloat previousPositionX = touch.PreviousLocationInView(View).X;
                    nfloat previousPositionY = touch.PreviousLocationInView(View).Y;

                    nfloat offsetX = previousPositionX - newPositionX;
                    nfloat offsetY = previousPositionY - newPositionY;

                    imgVDragMe.Frame = new CGRect(new CGPoint(imgVDragMe.Frame.X - offsetX, imgVDragMe.Frame.Y - offsetY),
                                                            imgVDragMe.Frame.Size);
                }
            }
        }

        //Touch-Event wird abgebrochen, z.B. durch InfoDialog oder andere Eigenschaften
        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
        }
        #endregion
    }
}