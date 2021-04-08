using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Notizen
{
    public partial class NotizListController : UITableViewController
    {
        public List<Notiz> notizList;
        TableSource tableSource;

        public NotizListController (IntPtr handle) : base (handle)
        {
            notizList = new List<Notiz>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            tableSource = new TableSource(this, notizList);
            TableView.Source = tableSource;
            notizList.Add(new Notiz("Test 01"));
            UIBarButtonItem barbtnEdit = new UIBarButtonItem(UIBarButtonSystemItem.Edit, (object sender, EventArgs args) =>
            {
                //Pruefen ob Editiermodus aktiv, dann beende den Editmodus
                if (TableView.Editing)
                {
                    TableView.SetEditing(false, true);
                }
                else
                {
                    TableView.SetEditing(true, true);
                }
            });
            NavigationItem.LeftBarButtonItem = barbtnEdit;

            //UIBarButtonItem barbtnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, addItemDialog);
            //NavigationItem.RightBarButtonItem = barbtnAdd;
            
            UIBarButtonItem barbtnDeleteAll = new UIBarButtonItem(UIBarButtonSystemItem.Trash, (sender, args) =>
            {
                notizList.Clear();
                TableView.ReloadData();
            });
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { barBtnItemAdd, barbtnDeleteAll };

            //GestureRecognizer der TableView hinzufuegen
            //UILongPressGestureRecognizer longPressGestureRecognizer = new UILongPressGestureRecognizer(longPress);
            //TableView.AddGestureRecognizer(longPressGestureRecognizer);
        }
        
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TableView.ReloadData();
        }

        //Methode zum hinzufuegen eines Eintrags
        private void addItemDialog(object sender, EventArgs args)
        {
            //Anlegen des UIAlertControllers mit Titel, Message und Style
            UIAlertController alertController = UIAlertController.Create(
                "Notiz-Item hinzufuegen",
                "Bite Titel eintragen",
                UIAlertControllerStyle.Alert);
            //Deklarieren ein UITextField
            UITextField txtInputItemName = null;

            //Hinzufuegen des TextFields zum AlertController
            alertController.AddTextField(addTextField =>
            {
                txtInputItemName = addTextField;
                txtInputItemName.Placeholder = "Hier Titel angeben";
            });

            alertController.AddAction(UIAlertAction.Create(
                "Speichern",
                UIAlertActionStyle.Default,
                speichern =>
                {
                    //Item der Liste hinzufuegen
                    notizList.Add(new Notiz(txtInputItemName.Text));
                    //Daten der TableView neu laden
                    TableView.ReloadData();
                }));
            //Da Dialog nur geschlosen wird, ist kein ActionHandler noetig
            alertController.AddAction(UIAlertAction.Create(
                "Abrrechen",
                UIAlertActionStyle.Cancel,
                null));

            //Anzeigen des AlertControllers
            PresentViewController(alertController, true, null);
        }

    }
}