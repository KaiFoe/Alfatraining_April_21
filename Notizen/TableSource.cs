using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Notizen
{
    //Klasse kuemmert sich um die Anzeige der Zellen
    //sowie Aktionen die auf den Zellen ausgefuehrt werden
    public class TableSource : UITableViewSource
    {

        string cellIdentifier = "cellID";
        List<Notiz> notizList;
        NotizListController notizListController;

        public TableSource(NotizListController notizListController, List<Notiz> notizList)
        {
            this.notizListController = notizListController;
            this.notizList = notizList;
        }

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
            Notiz item = notizList[indexPath.Row];
            //Wir schreiben das Item in die Zelle
            cell.TextLabel.Text = item.Title;
            cell.DetailTextLabel.Text = item.CreateDate.ToString();

            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return notizList.Count;
        }

        //Was passiert wenn wir einen Zeile selektieren
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //Instanz des DetailViewControllers erstellen
            DetailViewController detailViewController = (DetailViewController)notizListController.Storyboard.
                                                                    InstantiateViewController("DetailViewController");

            //Das Objekt der Zeile an den DetailViewController uebergeben
            detailViewController.notiz = notizList[indexPath.Row];
            //ueber NavigationController den DetailViewController oeffnen
            notizListController.NavigationController.PushViewController(detailViewController, true);
            
            
        }


        //Kann unsere TableView bzw koennen deren Eintraege bearbeitet werden
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    //Entferne das Item von der Datenquelle
                    notizList.RemoveAt(indexPath.Row);
                    //Entferne das Item aus der der TableView
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
            }
        }

        //ContextualAction zum bearbeiten eines Items
        public UIContextualAction editAction(NSIndexPath indexPath, UITableView tableView)
        {
            var action = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Bearbeiten",
                (UIContextualAction editItem, UIView view, UIContextualActionCompletionHandler success) =>
                {
                    //toDoListController.editItemDialog(indexPath);
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
                    //Item in der Liste loeschen
                    notizList.RemoveAt(indexPath.Row);
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
