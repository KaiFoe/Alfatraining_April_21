using System;
using UIKit;

namespace Animationen
{

    //In dieser Klasse verbinden wir den Übergang zwischen zwei ViewControllern mit der Animation
    public class TransitioningDelegate : UIViewControllerTransitioningDelegate
    {
        EigeneAnimation eigeneAnimation;

        public TransitioningDelegate()
        {
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            //Instanzieren der eigenen Animation
            eigeneAnimation = new EigeneAnimation();
            //Rueckgabe unserer eigenen Animation
            return eigeneAnimation;
        }
    }
}
