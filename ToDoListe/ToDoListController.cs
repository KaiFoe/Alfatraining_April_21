using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace ToDoListe
{
    public partial class ToDoListController : UITableViewController
    {
        
        TableSource tableSource;
        List<ToDo> itemList;
        DBHelper dbHelper;

        public ToDoListController (IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            dbHelper = new DBHelper();
            dbHelper.createDB();
            //dbHelper.GetDatabaseVersion();


            //Liste mit Strings zur Befuellung der TableView
            itemList = dbHelper.getAllToDos();
            //TableView in den Bearbeitungsmodus setzen
            //TableView.SetEditing(false, true);
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
            //NavigationItem.RightBarButtonItem = barbtnAdd;

            UIBarButtonItem barbtnDeleteAll = new UIBarButtonItem(UIBarButtonSystemItem.Trash, (sender, args) =>
            {
                itemList.Clear();
                dbHelper.deleteAllToDos();
                TableView.ReloadData();
            });
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { barbtnAdd, barbtnDeleteAll };

            //GestureRecognizer der TableView hinzufuegen
            UILongPressGestureRecognizer longPressGestureRecognizer = new UILongPressGestureRecognizer(longPress);
            TableView.AddGestureRecognizer(longPressGestureRecognizer);
        }

        //Methode zum hinzufuegen eines Eintrags
        private void addItemDialog(object sender, EventArgs args)
        {
            //Anlegen des UIAlertControllers mit Titel, Message und Style
            UIAlertController alertController = UIAlertController.Create(
                "ToDo-Item hinzufuegen",
                "Bite Namen eintragen",
                UIAlertControllerStyle.Alert);
            //Deklarieren ein UITextField
            UITextField txtInputItemName = null;

            //Hinzufuegen des TextFields zum AlertController
            alertController.AddTextField(addTextField =>
            {
                txtInputItemName = addTextField;
                txtInputItemName.Placeholder = "Hier ToDo angeben";
            });

            alertController.AddAction(UIAlertAction.Create(
                "Speichern",
                UIAlertActionStyle.Default,
                speichern =>
                {
                    //Item der Liste hinzufuegen
                    itemList.Add(new ToDo(txtInputItemName.Text));
                    dbHelper.addToDo(new ToDo(txtInputItemName.Text));
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

        public void editItemDialog(NSIndexPath indexPath)
        {
            //Rueckgabe des ausgewaehlten Items
            ToDo currentToDoObject = itemList[indexPath.Row];

            UIAlertController alertController = UIAlertController.Create(
                "Item bearbeiten",
                "Bitte neuen Item-Namen angeben",
                UIAlertControllerStyle.Alert);

            //Eingabefeld hinzufuegen
            UITextField txtInput = null;
            alertController.AddTextField(EditItem =>
            {
                txtInput = EditItem;
                txtInput.Text = currentToDoObject.Name;
            });

            //Button zum Speichern und Abbrechen hinzufuegen
            alertController.AddAction(UIAlertAction.Create(
                "Speichern",
                UIAlertActionStyle.Default,
                onClick =>
                {
                    //Pruefen ob Textfeld noch Inhalt hat
                    if (txtInput.Text.Length > 0)
                    {
                        currentToDoObject.Name = txtInput.Text;
                        dbHelper.updateToDo(currentToDoObject);
                        TableView.ReloadData();
                    }
                }));
            alertController.AddAction(UIAlertAction.Create(
                "Abbrechen",
                UIAlertActionStyle.Cancel,
                null
                ));

            //Dialog anzeigen
            PresentViewController(alertController, true, null);
        }

        //"LongPress" zum Bearbeiten eines Items
        private void longPress(UILongPressGestureRecognizer longPressGestureRecognizer)
        {
            //Sobald LongPress vom System erkannt wird...
            if (longPressGestureRecognizer.State == UIGestureRecognizerState.Began)
            {
                //wo wurde LongPress ausgeloest
                var point = longPressGestureRecognizer.LocationInView(TableView);
                //gebe Zeile zur|ck an der getappt wurde
                var indexPath = TableView.IndexPathForRowAtPoint(point);
                editItemDialog(indexPath);
            }

            
        }
    }
}
