using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace ToDoListe
{
    public class TableSource : UITableViewSource
    {
        List<ToDo> tableItems;
        //Notwendig, damit beim neuladen der TableViewCell die richtigen Inhalte geladen werden
        string cellIdentifier = "ToDoCell";
        //Notwendig um auf ViewController-Elemente zugreifen zu k<nnen
        ToDoListController toDoListController;
        DBHelper dBHelper;

        public TableSource(List<ToDo> items, ToDoListController toDoListController)
        {
            tableItems = items;
            this.toDoListController = toDoListController;
            dBHelper = new DBHelper();
        }

        //Gibt die Zelle und das Layout zurueck
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //Wir lassen uns aus der TableView eine Zeile geben
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            //Wenn noch keine Zeile existiert, lege eine neue Zeile an
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
            }
            //Wir lassen uns aus dem Array das Item welches zu dieser Zeile gehoert geben
            ToDo item = tableItems[indexPath.Row];
            //Wir schreiben das Item in die Zelle
            cell.TextLabel.Text = item.Name;
            cell.DetailTextLabel.Text = item.CreateDate.ToString() + " - " + item.EditDate.ToString(); 

            
            if (item.IsDone)
            {
                //Zusaetzliche Icons an der rechten Seite anzeigen
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            } else
            {
                cell.Accessory = UITableViewCellAccessory.None;
            }
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
            tableItems[indexPath.Row].IsDone = !tableItems[indexPath.Row].IsDone;
            dBHelper.updateToDo(tableItems[indexPath.Row]);

            tableView.ReloadData();
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

        //Kann unsere TableView bzw. koennen deren Eintraege verschoben werden
        public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void MoveRow(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
        {
            //Hole Item welches verschoben werden soll
            ToDo toDoItem = tableItems[sourceIndexPath.Row];
            //Position wo sich das Item befand
            int deleteAt = sourceIndexPath.Row;
            //neue Position des Items
            int insertAt = destinationIndexPath.Row;

            //Wird offenbar nicht mehr benoetigt
            //Wenn der Eintrag nach oben rutschen soll
            //if (insertAt < deleteAt)
            //    deleteAt += 1;
            //else
            //    insertAt += 1;

            //Eintrag an neuer Position hinzufuegen
            tableItems.Insert(insertAt, toDoItem);
            //Eintrag an alter Position loeschen
            tableItems.RemoveAt(deleteAt);
        }

        //ContextualAction zum bearbeiten eines Items
        public UIContextualAction editAction(NSIndexPath indexPath, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Bearbeiten",
                (UIContextualAction editItem, UIView view, UIContextualActionCompletionHandler success) =>
                {
                    toDoListController.editItemDialog(indexPath);
                    success(true);
                });
            action.BackgroundColor = UIColor.Orange;
            return action;
        }

        public UIContextualAction deleteAction(NSIndexPath indexPath, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Loeschen",
                (UIContextualAction deleteItem, UIView view, UIContextualActionCompletionHandler success) =>
                {
                    dBHelper.delete(tableItems[indexPath.Row]);
                    //Item in der Liste loeschen
                    tableItems.RemoveAt(indexPath.Row);
                    //Bestimmte Zeile(n) in der TableView loeschen
                    tableView.BeginUpdates();
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    tableView.EndUpdates();
                    
                    success(true);
                });
            action.BackgroundColor = UIColor.Blue;
            return action;
        }
        //Fuer das Wischen von rechts nach links
        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var swipeDeleteAction = deleteAction(indexPath, tableView);
            var swipeEditAction = editAction(indexPath, tableView);
            var trailingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { swipeEditAction, swipeDeleteAction });
            return trailingSwipe;
        }

        //Fuer das wischen von links nach rechts
        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var swipeEditAction = editAction(indexPath, tableView);
            var swipeDeleteAction = deleteAction(indexPath, tableView);
            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { swipeEditAction, swipeDeleteAction });

            return leadingSwipe;
        }
    }
}
