using CoreLocation;
using Foundation;
using MapKit;
using System;
using System.Collections.Generic;
using UIKit;

namespace Karten_Uebung
{
    public partial class ViewController : UIViewController
    {

        private CLLocationManager locationManager;
        private CLLocation location;
        private List<CLLocationCoordinate2D> route = new List<CLLocationCoordinate2D>();
        private UISearchController searchController;
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            //initializeSearchBar();

            MapDelegate mapDelegate = new MapDelegate();
            mapView.Delegate = mapDelegate;

            //locationManager initialisieren
            locationManager = new CLLocationManager();
            //Nutzer nach Berechtigung fragen
            locationManager.RequestAlwaysAuthorization();
            //Event "LocationsUpdated" wird ausgeloest wenn sich die Position geaendert hat
            locationManager.LocationsUpdated += LocationManager_LocationsUpdated;
            locationManager.StartUpdatingLocation();

            //SearchBar
            var searchResultsController = new SearchResultsViewController(mapView);
            var searchUpdater = new SearchResultsUpdater();
            searchUpdater.UpdateSearchResults += searchResultsController.UpdateSearchResults;

            searchController = new UISearchController(searchResultsController);
            searchController.SearchResultsUpdater = searchUpdater;

            searchController.SearchBar.SizeToFit();
            searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            searchController.SearchBar.Placeholder = "Gebe einen Ort oder eine Adresse ein";
            searchController.HidesNavigationBarDuringPresentation = false;
            NavigationItem.TitleView = searchController.SearchBar;
            DefinesPresentationContext = true;
        }

        private void LocationManager_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            //Gibt uns die Koordinaten unserer Position zurueck
            location = e.Locations[0];
            //Aktuellen Punkt der Route hinzufuegen
            route.Add(location.Coordinate);
            MKPolyline polyline = MKPolyline.FromCoordinates(route.ToArray());
            //Linie der MapView hinzufuegen
            mapView.AddOverlay(polyline);

            //Bereich der Karte instanzieren
            MKCoordinateRegion region = new MKCoordinateRegion();
            //Unser Standort soll der Mittelpunkt der Karte werden
            region.Center = location.Coordinate;
            //Verbinden Bereich mit der Karten
            mapView.SetRegion(region, true);
            //Zeige die aktuelle Position an
            mapView.ShowsUserLocation = true;

            
        }

        private void initializeSearchBar()
        {
            var searchResultsController = new SearchResultsViewController(mapView);
            var searchUpdater = new SearchResultsUpdater();
            searchUpdater.UpdateSearchResults += searchResultsController.UpdateSearchResults;

            UISearchController searchController = new UISearchController(searchResultsController);
            searchController.SearchResultsUpdater = searchUpdater;

            searchController.SearchBar.SizeToFit();
            searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            searchController.SearchBar.Placeholder = "Gebe einen Ort oder eine Adresse ein";
            
            NavigationItem.TitleView = searchController.SearchBar;
            DefinesPresentationContext = true;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}