using System;
using System.Collections.Generic;
using UIKit;

namespace Zeichnen
{

    public class NamedValue<T>
    {
        public string Name { get; set; }
        //T = Platzhalter fuer den Datentyp
        //erlaubt die Klasse fuer mehrere Aufgaben zu benutzen
        public T Value { get; set; }

        public NamedValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }

    public class PickerDataModel<T> : UIPickerViewModel 
    {
        //Liste mit Items
        public List<NamedValue<T>> Items { get; private set; }
        //Index zur Bestimmung welches Item ausgewaehlt wurde
        int selectedIndex;
        //Event um Aenderungen der Auswahl zu erfassen
        public event EventHandler<EventArgs> valueChanged;
        
        public PickerDataModel()
        {
            Items = new List<NamedValue<T>>();
        }

        public NamedValue<T> SelectedItem
        {
            get
            {
                //Wenn Items nicht leer
                //und selectedIndex groeßer oder gleich 0
                //und selectedIndex kleiner als die Anzahl der Elemente in der Liste
                //dann gebe ausgwaehltes Item zurueck, ansonsten gebe null zurueck;
                /*if (Items != null)
                {
                    if (selectedIndex >= 0 && selectedIndex < Items.Count)
                        return Items[selectedIndex];
                    else
                        return null;
                }
                else
                    return null;*/
                //return "Bedingung" ? Dann : Sonst
                return Items != null && selectedIndex >= 0 && selectedIndex < Items.Count ? Items[selectedIndex] : null;
            }
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            //Wenn Liste befuellt ist, gebe die Anzahl der Elemente zurueck, ansonsten 0
            return Items != null ? Items.Count : 0;
        }

        
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            //Wenn die Zeile innerhalb der Liste liegt, gebe den Namen zurueck
            return Items.Count > row ? Items[(int)row].Name : null;
        }

        //Anzahl der Auswahlrollen (wir brauchen nur eine, bei Datum braeuchte man fuer Tag, Monat, Jahr 3 Rollen)
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        
        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = (int)row;
            valueChanged?.Invoke(this, new EventArgs());
        }
    }
}
