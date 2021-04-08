using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TableView.logic
{
    public class TableSource : UITableViewSource
    {
        List<string> tableItems;
        //Notwendig, damit beim neuladen der TableViewCell die richtigen Inhalte geladen werden
        string cellIdentifier = "TableCell";
        //Notwendig um auf ViewController-Elemente zugreifen zu k<nnen
        TableViewController tableViewController;

        public TableSource(List<string> items, TableViewController tableViewController)
        {
            tableItems = items;
            this.tableViewController = tableViewController;
        }

        //Gibt die Zelle und das Layout zurueck
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //Wir lassen uns aus der TableView eine Zeile geben
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            //Wenn noch keine Zeile existiert, lege eine neue Zeile an
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            }
            //Wir lassen uns aus dem Array das Item welches zu dieser Zeile gehoert geben
            string item = tableItems[indexPath.Row];
            //Wir schreiben das Item in die Zelle
            cell.TextLabel.Text = item;
            //Zusaetzliche Icons an der rechten Seite anzeigen
            cell.Accessory = UITableViewCellAccessory.Checkmark;
            //Wir geben die Zelle zurueck
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //gibt die Anzahl der Elemente zurueck
            return tableItems.Count;
        }

        //Wird ausgel<st, wenn wir einen TableView-Eintrag angeklickt haben
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //Infodialog erstellen mit Angabe von Titel, Message und Style
            UIAlertController okAlertController = UIAlertController.Create("Zeile ausgewaehlt",
                                                                            tableItems[indexPath.Row],
                                                                            UIAlertControllerStyle.Alert);
            //OK-Button hinzufuegen
            okAlertController.AddAction(UIAlertAction.Create("OK",
                                                                UIAlertActionStyle.Default,
                                                                null));
            //Infodialog in der TableView anzeigen mithilfe des PresentViewControllers
            tableViewController.PresentViewController(okAlertController, true, null);
            //Hervorheben der Zeile wieder beenden
            tableView.DeselectRow(indexPath, true);
        }

        //Kann unsere TableView bzw koennen deren Eintraege bearbeitet werden
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch(editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    //Entferne das Item von der Datenquelle
                    tableItems.RemoveAt(indexPath.Row);
                    //Entferne das Item aus der der TableView
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
            }
        }
    }
}
