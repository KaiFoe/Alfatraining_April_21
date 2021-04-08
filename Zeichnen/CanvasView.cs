using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Zeichnen
{
    public class CanvasView : UIView
    {
        //Liste aller fertig gezeichneten Linien
        List<Polyline> fertigeLinien = new List<Polyline>();
        //Dictionary welches die aktuell zu zeichnenden Linien enthaelt
        Dictionary<IntPtr, Polyline> aktuelleLinien = new Dictionary<IntPtr, Polyline>();

        //Attribute mit Defaultwerten
        //wichtig damit zu Beginn direkt mit Farbe und Stiftstaerke gearbeitet werden kann
        public CGColor Linienfarbe { get; set; } = new CGColor(0.7f, 0, 0);
        public float Linienstaerke { get; set; } = 2;

        public CanvasView()
        {
            BackgroundColor = UIColor.White;
            MultipleTouchEnabled = false;
        }

        //Zeichnen der CanvasView
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                //Setze Aussehen von Linienanfang und Linienende
                context.SetLineCap(CGLineCap.Square);
                context.SetLineJoin(CGLineJoin.Round);

                foreach (Polyline linie in fertigeLinien)
                {
                    //Setze Farbe, Breite und Pfad
                    context.SetStrokeColor(linie.Color);
                    context.SetLineWidth(linie.Breite);
                    context.AddPath(linie.Pfad);

                    //Zeichne die Linie
                    context.DrawPath(CGPathDrawingMode.Stroke);
                }

                foreach (Polyline linie in aktuelleLinien.Values)
                {
                    //Setze Farbe, Breite und Pfad
                    context.SetStrokeColor(linie.Color);
                    context.SetLineWidth(linie.Breite);
                    context.AddPath(linie.Pfad);

                    //Zeichne die Linie
                    context.DrawPath(CGPathDrawingMode.Stroke);
                }
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            //fuer jedes gesartete TouchEvent
            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                //Definieren einer neuen Linie
                Polyline linie = new Polyline
                {
                    Color = Linienfarbe,
                    Breite = Linienstaerke
                };

                //Bewegen des Startpunktes des Pfades auf den Punkt wo wir das Display beruehren
                linie.Pfad.MoveToPoint(touch.LocationInView(this));
                //Linie dem Dictionary fuer aktuelle Linien uebergeben
                aktuelleLinien.Add(touch.Handle, linie);
            }

            //Aenderung an die CanvasView uebergeben (neu zeichnen)
            SetNeedsDisplay();
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            //Loesche die Linie(n) die gerade gezeichnet wird/werden
            aktuelleLinien.Clear();
            //Aenderungen an die CanvasView uebergeben (neu zeichnen)
            SetNeedsDisplay();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                //Bestimme Polyline
                Polyline linie = aktuelleLinien[touch.Handle];
                //Entferne Polyline aus Dictionary der aktuell zu zeichnenden Linien
                aktuelleLinien.Remove(touch.Handle);
                //Fuege der Linie den letzten Punkt hinzu
                linie.Pfad.AddLineToPoint(touch.LocationInView(this));
                //fuege Linie der Liste fertiger Linien hinzu
                fertigeLinien.Add(linie);
            }

            SetNeedsDisplay();
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            //fuer jede Touchbewegung auf der CanvasView
            foreach(UITouch touch in touches.Cast<UITouch>()){
                //fuegen wir der atkuellen Linie am Ende den neuen Punkt (aus der Bewegung) hinzu
                aktuelleLinien[touch.Handle].Pfad.AddLineToPoint(touch.LocationInView(this));
            }
            SetNeedsDisplay();
        }

        public void clearView()
        {
            fertigeLinien.Clear();
            SetNeedsDisplay();
        }
    }
}
