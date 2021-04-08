using System;
using System.Collections.Generic;
using Foundation;
using MapKit;
using UIKit;

namespace Karten_Uebung
{
    public class SearchResultsViewController : UITableViewController
    {

        private string cellIdentifier = "mapItemCellId";
        private List<MKMapItem> items;
        private MKMapView map;

        public SearchResultsViewController(MKMapView map)
        {
            this.map = map;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            //Gebe die Anzahl der Items zurueck
            //Ist Items-Liste null, dann gebe 0 zurueck
            return items?.Count ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            /*var cell = tableView.DequeueReusableCell(cellIdentifier);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            */
            var cell = tableView.DequeueReusableCell(cellIdentifier) ?? new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);

            cell.TextLabel.Text = items[indexPath.Row].Name;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var coordinate = items[indexPath.Row].Placemark.Location.Coordinate;

            map.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = coordinate,
                Title = items[indexPath.Row].Name
            });

            map.SetCenterCoordinate(coordinate, true);
            DismissViewController(false, null);
        }

        public void UpdateSearchResults(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                //Erstellen einer Suachanfrage
                var searchRequest = new MKLocalSearchRequest
                {
                    NaturalLanguageQuery = query,
                    Region = new MKCoordinateRegion(map.UserLocation.Coordinate, new MKCoordinateSpan(0.25, 0.25))
                };

                var localSearch = new MKLocalSearch(searchRequest);
                localSearch.Start((response, error) =>
                {
                    if (response != null && error == null)
                    {
                        items = new List<MKMapItem>(response.MapItems);
                        TableView.ReloadData();
                    }
                });
            }
        }

    }
}
