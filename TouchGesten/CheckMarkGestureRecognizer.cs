using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace TouchGesten
{
    public class CheckMarkGestureRecognizer : UIGestureRecognizer
    {

        private CGPoint midpoint = CGPoint.Empty;
        private bool strokeUp = false;

        public CheckMarkGestureRecognizer()
        {
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            if (touches.Count != 1)
                base.State = UIGestureRecognizerState.Failed;
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            base.State = UIGestureRecognizerState.Failed;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            if (base.State == UIGestureRecognizerState.Possible && strokeUp)
                base.State = UIGestureRecognizerState.Recognized;
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            //pruefen ob State nicht failed
            if (base.State != UIGestureRecognizerState.Failed)
            {
                CGPoint currentPoint = (touches.AnyObject as UITouch).LocationInView(View);
                CGPoint previousPoint = (touches.AnyObject as UITouch).PreviousLocationInView(View);

                if (!strokeUp)
                {
                    //wenn wir uns nach unten bewegen, setzen wir den neuen Punkt immer als Mittelpunkt
                    //da wir nicht wissen, wann der Nutzer den Finger nach oben bewegt
                    if (currentPoint.X >= previousPoint.X && currentPoint.Y >= previousPoint.Y)
                        midpoint = currentPoint;
                    //Wenn wir nach oben gehen, setze die HVariable strokeUp auf true
                    else if (currentPoint.X >= midpoint.X && currentPoint.Y <= midpoint.Y)
                        strokeUp = true;
                    else
                        base.State = UIGestureRecognizerState.Failed;
                }
            }
        }

        //aufgerufen wird Reset, wenn UIGestureRecognizerState = Failed
        public override void Reset()
        {
            base.Reset();
            strokeUp = false;
            midpoint = CGPoint.Empty;
        }
    }
}
