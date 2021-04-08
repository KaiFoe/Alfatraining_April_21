using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Zitate
{
    public class TableSource : UITableViewSource
    {

        private List<Zitat> zitateList;
        static string cellIdentifier = "Zitat";

        public TableSource(List<Zitat> zitateList)
        {
            this.zitateList = zitateList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);

            //Befuellen der Zelle
            cell.TextLabel.Text = zitateList[indexPath.Row].Text;
            cell.DetailTextLabel.Text = zitateList[indexPath.Row].Author;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return zitateList.Count;
        }
    }
}
