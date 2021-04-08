using System;
using UIKit;

namespace WetterApp
{
    //Beschreibt unser Wetter_Objekt mit allen benoetigten Informationen
    public class Wetter
    {

        //Attribute
        public string Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Windspeed { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public UIImage Image { get; set; }

        public Wetter()
        {
        }

        public Wetter(string temp, string pressure, string humidity, string windspeed, string imageId, string description)
        {
            Temp = temp;
            Pressure = pressure;
            Humidity = humidity;
            Windspeed = windspeed;
            ImageId = imageId;
            Description = description;
        }
    }
}
