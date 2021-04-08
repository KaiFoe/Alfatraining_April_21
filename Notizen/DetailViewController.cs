using Foundation;
using System;
using UIKit;

namespace Notizen
{
    public partial class DetailViewController : UIViewController
    {
        public Notiz notiz;

        public DetailViewController (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (notiz != null)
            {
                fillFields(notiz);
            }
                

            barBtnItemSave.Clicked += BarBtnItemSave_Clicked;
        }

        private void BarBtnItemSave_Clicked(object sender, EventArgs e)
        {
            if (notiz != null)
            {
                notiz.Title = txtTitel.Text;
                notiz.Description = txtDescription.Text;
                notiz.EditDate = DateTime.Now;
                //DetailViewController schließen und zum vorherigen ViewController zurueckkehren
                NavigationController.PopViewController(true);

                //Alternativen
                //zu einem bestimmten Controller zurueckkehren (viewController = Objekt des ViewControllers)
                //NavigationController.PopToViewController(viewController, true);
                //zum RootViewController zurueckkehren
                //NavigationController.PopToRootViewController(true);
            }
            else
            {
                Notiz newNotiz = new Notiz();
                newNotiz.Title = txtTitel.Text;
                newNotiz.Description = txtDescription.Text;

                //Über den NavigationController Instanz des vorherigen ViewControllers zuweisen
                var parentViewController = (NotizListController)NavigationController.ViewControllers[0];
                //Erstelltes Notizobjekt der Liste hinzufuegen
                parentViewController.notizList.Add(newNotiz);
            }
        }

        private void fillFields(Notiz notiz)
        {
            txtTitel.Text = notiz.Title;
            txtDescription.Text = notiz.Description;
            lblCreateDate.Text = notiz.CreateDate.ToString();
            if (notiz.EditDate != null)
                lblEditDate.Text = notiz.EditDate.ToString();
        }

        
    }
}