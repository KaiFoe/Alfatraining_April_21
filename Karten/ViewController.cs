using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace Karten
{
    public partial class ViewController : UIViewController
    {

        private CLLocationManager locationManager;
        private CLLocation location;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            locationManager = new CLLocationManager();
            locationManager.RequestAlwaysAuthorization();

            btnStart.TouchUpInside += TouchUpInside;
            btnStop.TouchUpInside += TouchUpInside;

            MapDelegate mapDelegate = new MapDelegate();
            mapView.Delegate = mapDelegate;
            //Definieren eines Polygons anhand von Koordinaten
            MKPolygon areaOverlay = MKPolygon.FromCoordinates(
                new CLLocationCoordinate2D[]
                {
                    new CLLocationCoordinate2D(48.858261, 2.294507),
                    new CLLocationCoordinate2D(48.858361, 2.294507),
                    new CLLocationCoordinate2D(48.858361, 2.294607),
                    new CLLocationCoordinate2D(48.858261, 2.294607),
                    new CLLocationCoordinate2D(48.858261, 2.294507),
                });
            mapView.AddOverlay(areaOverlay);

            MKCircle circle = MKCircle.Circle(new CLLocationCoordinate2D(48.858261, 2.294507), 100);
            mapView.AddOverlay(circle);
        }

        private void TouchUpInside(object sender, EventArgs e)
        {
            //wenn Start-Button getouched wird
            if (sender == btnStart)
            {
                lblOutput.Text = "Bestimme Standort...";
                //Starte U
                locationManager.StartUpdatingLocation();
                locationManager.LocationsUpdated += LocationManager_LocationsUpdated;
            }
        }

        private void LocationManager_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            //Location aus den uebergebenen Argumenten auslesen
            location = e.Locations[0];
            double latitude = Math.Round(location.Coordinate.Latitude, 4);
            double longitude = Math.Round(location.Coordinate.Longitude, 4);
            double accuracy = Math.Round(location.HorizontalAccuracy, 0);

            lblOutput.Text = string.Format("Latitude: {0}\nLongitude: {1}\nAccuracy: {2}m", latitude, longitude, accuracy);

            //Bereich der Karte instanzieren
            MKCoordinateRegion region = new MKCoordinateRegion();
            //Der Mittelpunkt des Bereich sollen unsere Koordinaten sein
            region.Center = location.Coordinate;

            //Setze den Bereich dier MapView
            mapView.SetRegion(region, true);

            //UserPosition anzeigen
            mapView.ShowsUserLocation = true;

            //Kartentyp angeben
            mapView.MapType = MKMapType.Satellite;

            //Zoomlevel festlegen
            mapView.ZoomEnabled = true;

            //Pin auf der Map erstellen
            MKPointAnnotation eifelturm = new MKPointAnnotation();
            eifelturm.Coordinate = new CLLocationCoordinate2D(48.858261, 2.294507);
            eifelturm.Title = "Eifelturm";

            ConferenceAnnotation eifel = new ConferenceAnnotation("Eifelturm", new CLLocationCoordinate2D(48.858261, 2.294507));
            mapView.AddAnnotation(eifel);
            //Den Pin der Karte hinzufuegen
            //mapView.AddAnnotation(eifelturm);
            //mapView.AddAnnotations(new IMKAnnotation[] { eifelturm });

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}