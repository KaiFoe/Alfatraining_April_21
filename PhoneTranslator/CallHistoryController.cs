using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace PhoneTranslator
{
    public partial class CallHistoryController : UITableViewController
    {
        internal List<string> telephoneList;

        public CallHistoryController (IntPtr handle) : base (handle)
        {
            telephoneList = new List<string>();
            

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.Source = new TableSource(telephoneList);
            TableView.ReloadData();
        }
    }
}