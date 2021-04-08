using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Animationen
{
    public partial class ViewController : UIViewController
    {
        CALayer layer;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnCustomAnimation.TouchUpInside += (sender, args) =>
            {
                //Instanzieren den ViewController der geoffnet werden soll
                ThirdViewController thirdViewController = Storyboard.InstantiateViewController("ThirdViewController") as ThirdViewController;
                //Zuweisung einer Animation
                thirdViewController.ModalPresentationStyle = UIModalPresentationStyle.Custom;
                //Aufladen der eigenen Animation an den ThirdViewController
                thirdViewController.TransitioningDelegate = new TransitioningDelegate();
                //Öffnen des ThirdViewControllers
                PresentViewController(thirdViewController, true, null);
            };
            //Initialiiseren unseres Objekts welches wir spaeter animieren wollen
            layer = new CALayer();
            layer.Bounds = new CGRect(0, 0, 50, 50);
            layer.Position = new CGPoint(50, 250);
            layer.Contents = UIImage.FromFile("DoubleTapMe.png").CGImage;
            layer.ContentsGravity = CALayer.GravityResize;
            layer.BorderWidth = 1.5f;
            layer.BorderColor = UIColor.Purple.CGColor;

            View.Layer.AddSublayer(layer);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            /*CATransaction.Begin();
            CATransaction.AnimationDuration = 10;
            layer.Position = new CGPoint(50, 400);
            layer.BorderWidth = 5.0f;
            layer.BorderColor = UIColor.Green.CGColor;
            CATransaction.Commit();*/

            //festlegen der Start-Position
            CGPoint fromPoint = layer.Position;
            //Festlegen der Ziel-Position
            layer.Position = new CGPoint(200, 500);
            //Festlegen eines Verlaufspfades an dem die Animation laeuft
            CGPath path = new CGPath();
            path.AddLines(new CGPoint[] { fromPoint,
                                            new CGPoint(50, 350),
                                            new CGPoint(200, 150),
                                            new CGPoint(200, 200)});
            CAKeyFrameAnimation animationPosition = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath("position");
            animationPosition.Path = path;
            animationPosition.Duration = 5;

            layer.AddAnimation(animationPosition, "position");

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.


        }
    }
}