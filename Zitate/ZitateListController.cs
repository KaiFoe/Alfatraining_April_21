using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Zitate
{
    public partial class ZitateListController : UITableViewController
    {
        public List<Zitat> zitateList;
        Utility utility;
        NSUserDefaults prefList;
        string anzahl, mode;

        public ZitateListController (IntPtr handle) : base (handle)
        {
            zitateList = new List<Zitat>();
            utility = new Utility();
            prefList = NSUserDefaults.StandardUserDefaults;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            

            TableView.Source = new TableSource(zitateList);

            UIBarButtonItem barBtnRefresh = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (sender, args) =>
            {
                loadData(anzahl, mode);
            });

            UIBarButtonItem barBtnSettings = new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender, args) =>
            {
                var viewController = (ViewController)Storyboard.InstantiateViewController("ViewController");
                NavigationController.PushViewController(viewController, true);
            });
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { barBtnRefresh, barBtnSettings };
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (string.IsNullOrEmpty(prefList.StringForKey("Anzahl")))
                anzahl = "0";
            else
                anzahl = prefList.StringForKey("Anzahl");

            bool modus = prefList.BoolForKey("Modus");

            if (modus)
                mode = "0";
            else
                mode = "1";

            loadData(anzahl, mode);
        }

        private void loadData(string anzahl, string modus)
        {
            //Wenn JsonMode aktiv
            if (modus.Equals("0"))
            {
                zitateList.Clear();
                zitateList.AddRange(utility.jsonGetZitate(anzahl, modus));
                //zitateList = utility.jsonGetZitate(anzahl, modus);
                TableView.ReloadData();
            }
            
        }
    }
}