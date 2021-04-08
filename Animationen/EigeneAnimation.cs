using System;
using CoreGraphics;
using UIKit;

namespace Animationen
{
    public class EigeneAnimation : UIViewControllerAnimatedTransitioning
    {
        public EigeneAnimation()
        {
        }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            //HauptView in der unsere Animation ausgefuehrt wird
            var inView = transitionContext.ContainerView;
            //Instanz der Subview (die animiert wird)
            var toViewController = transitionContext.GetViewControllerForKey(UITransitionContext.ToViewControllerKey);
            //ViewObjekt der Subview instanzieren
            var toView = toViewController.View;
            //ViewObjekt der Subview der Hauptview uebergeben
            inView.AddSubview(toView);

            //Wird benoetigt, damit wir die moegliche Groeße des zur Verfuegung stehen Bereichs kennen
            var frame = toView.Frame;
            //Der Bereich in dem die Subview angezeigt wird, soll zu Beginn leer sein (0px breit und 0px hoch)
            toView.Frame = CGRect.Empty;

            //Methode Animate erwartet 3 Parameter
            //1. Dauer der Animation
            //2. Action die ausgefuehrt wird (Animation)
            //3. Was wird nach erfolgreicher Ausfuehrung gemacht
            UIView.Animate(TransitionDuration(transitionContext), () =>
            {
                //Wir ruecken unsere SubView von links oben um 20 Pixel ein
                //damit wir rechts und unten ebenfalls einen Rand von 20 Pixel haben
                //muessen Breite und Hoehe um 40 Pixel verkleinert werden
                toView.Frame = new CGRect(20, 20, frame.Width - 40, frame.Height - 40);
                toView.BackgroundColor = UIColor.Red;
            }, () =>
            {
                transitionContext.CompleteTransition(true);
            });
        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return 2.0;
        }
    }
}
