using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace PhoneTranslator
{
    public class TableSource : UITableViewSource
    {
        List<string> telephoneList;
        //Notwendig, damit beim neuladen der TableViewCell die richtigen Inhalte geladen werden
        string cellIdentifier = "TableCell";
        //Notwendig um auf ViewController-Elemente zugreifen zu k<nnen
        

        public TableSource(List<string> items)
        {
            telephoneList = items;
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
            string item = telephoneList[indexPath.Row];
            //Wir schreiben das Item in die Zelle
            cell.TextLabel.Text = item;
            //Wir geben die Zelle zurueck
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //gibt die Anzahl der Elemente zurueck
            return telephoneList.Count;
        }
    }
}
