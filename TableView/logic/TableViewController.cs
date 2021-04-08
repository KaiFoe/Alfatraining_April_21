using Foundation;
using System;
using System.Collections.Generic;
using TableView.logic;
using UIKit;

namespace TableView
{
    public partial class TableViewController : UITableViewController
    {
        //Wird ben<tigt um den Editiermodus an und ausschalten zu koennen
        bool editMode = false;
        TableSource tableSource;
        List<string> itemList;


        
        public TableViewController (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Liste mit Strings zur Befuellung der TableView
            itemList = new List<string> { "Apple", "Samsung", "Motorola", "Sony", "Huawai", "Andere" };
            //TableView in den Bearbeitungsmodus setzen
            TableView.SetEditing(false, true);
            //TableView.Source mit der TableSource verbinden und StringListe uebergen
            tableSource = new TableSource(itemList, this);
            TableView.Source = tableSource;
            //Neueinlesen der Daten anstoßen
            //Immer dann notwendig, wenn TableView schon gezeichnet wurde
            TableView.ReloadData();

            //Wir legen einen UIBarButtonItem an, der in der Navigationsleiste/Titelleiste angezeigt werden soll
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

            UIBarButtonItem barbtnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add, addItemDialog);
            NavigationItem.RightBarButtonItem = barbtnAdd;

        }

        //Methode zum hinzufuegen eines Eintrags
        private void addItemDialog(object sender, EventArgs args)
        {
            //Anlegen des UIAlertControllers mit Titel, Message und Style
            UIAlertController alertController = UIAlertController.Create(
                "Item hinzufuegen",
                "Bite Namen eintragen",
                UIAlertControllerStyle.Alert);
            //Deklarieren ein UITextField
            UITextField txtInputItemName = null;

            //Hinzufuegen des TextFields zum AlertController
            alertController.AddTextField(addTextField =>
            {
                txtInputItemName = addTextField;
                txtInputItemName.Placeholder = "Hier ItemName angeben";
            });

            alertController.AddAction(UIAlertAction.Create(
                "Speichern",
                UIAlertActionStyle.Default,
                speichern =>
                {
                    //Item der Liste hinzufuegen
                    itemList.Add(txtInputItemName.Text);
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