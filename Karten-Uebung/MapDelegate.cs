using System;
using MapKit;
using UIKit;

namespace Karten_Uebung
{
    public class MapDelegate : MKMapViewDelegate
    {
        //Bestimme das Layout der OverlayObjekte
        public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
        {

            MKPolygon polygon = overlay as MKPolygon;
            MKCircle circle = overlay as MKCircle;
            MKPolyline polyline = overlay as MKPolyline;

            if (polygon != null)
            {
                MKPolygonView polygonView = new MKPolygonView(polygon);
                polygonView.FillColor = UIColor.Red;
                polygonView.StrokeColor = UIColor.Blue;
                return polygonView;
            }

            else if (circle != null)
            {
                MKCircleView circleView = new MKCircleView(circle);
                circleView.FillColor = UIColor.FromRGBA(0, 255, 0, 60);
                return circleView;
            }

            else if (polyline != null)
            {
                MKPolylineView polylineView = new MKPolylineView(polyline);
                polylineView.FillColor = UIColor.Green;
                polylineView.LineWidth = 20;
                polylineView.StrokeColor = UIColor.Magenta;
                return polylineView;
            }

            return null;
        }
    }
}
