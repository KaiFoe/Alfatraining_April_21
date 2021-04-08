using System;
using CoreGraphics;

namespace Zeichnen
{
    public class Polyline
    {
        //Attribute die unsere Polyline beschreiben
        //CGPath enthaelt eine Sammlung von Punkten
        //dient dazu wie und wo die Linie gezeichnet wird
        public CGPath Pfad { get; set; }
        //Breite oder Staerke unserer Linie
        public float Breite { get; set; }
        //Farbe unserer Linie
        public CGColor Color { get; set; }

        public Polyline()
        {
            Pfad = new CGPath();
        }
    }
}
