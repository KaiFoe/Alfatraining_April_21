using System;
using CoreLocation;
using MapKit;

namespace Karten
{
    public class ConferenceAnnotation : MKAnnotation
    {

        private CLLocationCoordinate2D coordinate;
        private readonly string title;


        public ConferenceAnnotation(string title, CLLocationCoordinate2D coordinate)
        {
            this.title = title;
            this.coordinate = coordinate;
        }

        public override string Title => title;
        public override CLLocationCoordinate2D Coordinate => coordinate;
    }
}
