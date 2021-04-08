using System;
using MapKit;
using UIKit;

namespace Karten
{
    public class MapDelegate : MKMapViewDelegate
    {
        //Bestimme das Layout der OverlayObjekte
        public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
        {
            MKPolygon polygon = overlay as MKPolygon;
            MKCircle circle = overlay as MKCircle;

            if (polygon != null)
            {
                MKPolygonView polygonView = new MKPolygonView(polygon);
                polygonView.FillColor = UIColor.Red;
                polygonView.StrokeColor = UIColor.Blue;

                return polygonView;
            }

            if (circle != null)
            {
                MKCircleView circleView = new MKCircleView(circle);
                circleView.FillColor = UIColor.FromRGBA(0, 255, 0, 60);
                return circleView;
            }

            return null;
        }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            if (annotation is ConferenceAnnotation)
            {
                annotationView = mapView.DequeueReusableAnnotation("conference");
                if (annotationView == null)
                    annotationView = new MKAnnotationView(annotation, "conference");


                annotationView.CanShowCallout = true;
                annotationView.Image = UIImage.FromBundle("CheckBox_Start.png");
            }

            return annotationView;
        }
    }
}
