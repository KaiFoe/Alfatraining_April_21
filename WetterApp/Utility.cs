using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Foundation;
using Newtonsoft.Json.Linq;
using UIKit;

namespace WetterApp
{
    public class Utility
    {
        static string appid = "03032cc118e2005750fddd997d939a2a";
        static string imageUrl = "https://openweathermap.org/img/w/";

        public Utility()
        {
        }

        //Wir brauchen eine Methode um das jsonFile vom Server zu holen
        public string getRequest(string city)
        {
            var request = WebRequest.Create(string.Format(@"https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, appid));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //Pruefen ob StatusCode OK
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "Error fetching data. Server returns status code " + response.StatusCode;
                }
                else
                {
                    //using kuemmnert sich darum, dass die Instanz vom StreamReader nach verlassen des Blocks
                    //automatisch geschlossen wird
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        //Auslesen des Inhalts
                        var content = reader.ReadToEnd();
                        //Bekommen wir keinen oder leeren Inhalt, geben wir eine Fehlermeldung aus
                        if (string.IsNullOrWhiteSpace(content))
                            return "Response contained empty body";
                        else
                            return content;
                    }

                    //Aequivalent zu using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    //StreamReader reader = new StreamReader(response.GetResponseStream());
                    //Abarbeiten unseres Codes
                    //reader.Close();
                }

            }
        }

        //Eine Methode um das Json-File zu zerlegen und die benoetigten Informationen auszulesen
        public Wetter jsonGetWetter(string city)
        {
            Wetter wetter = new Wetter();

            try
            {
                //Json-File als String von Webseite holen
                var content = getRequest(city);

                var resultObject = AllChildren(JObject.Parse(content));
                var weatherarray = resultObject.First(child => child.Type == JTokenType.Array && child.Path.Contains("weather"));
                var mainObject = resultObject.First(child => child.Type == JTokenType.Object && child.Path.Contains("main"));
                var windObject = resultObject.First(child => child.Type == JTokenType.Object && child.Path.Contains("wind"));

                foreach (JObject result in weatherarray)
                {
                    if (result.ContainsKey("icon"))
                        wetter.ImageId = result.GetValue("icon").ToString(); 

                }

                if (mainObject != null)
                {
                    wetter.Temp = mainObject.SelectToken("temp").ToString();
                }


                var resultObject2 = JObject.Parse(content);
                wetter.Temp = resultObject2.SelectToken("main.temp").ToString();
                wetter.Humidity = resultObject2.SelectToken("main.humidity").ToString();
                wetter.Pressure = resultObject2.SelectToken("main.pressure").ToString();
                wetter.ImageId = resultObject2.SelectToken("weather[0].icon").ToString();
                wetter.Windspeed = resultObject2.SelectToken("wind.speed").ToString();
                wetter.Description = resultObject2.SelectToken("weather[0].description").ToString();

                wetter.Image = imageFromUrl(imageUrl + wetter.ImageId + ".png");

            } catch(Exception ex)
            {

            }

            return wetter;
        }


        private UIImage imageFromUrl(string uri)
        {
            //Aus String eine Url bauen
            using (var url = new NSUrl(uri))
            {
                //Daten aus der Url holen
                using (var data = NSData.FromUrl(url))
                {
                    return UIImage.LoadFromData(data);
                }
            }
        }
        //Methode um jeden Knotenpunkt/Token im JsonObjekt zu erreichen
        private static IEnumerable<JToken> AllChildren(JToken jToken)
        {
            foreach (var child in jToken.Children())
            {
                yield return child;
                foreach (var childchild in AllChildren(child))
                    yield return childchild;
            }
        }
    }
}
