using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Zitate
{
    public class Utility
    {
        public Utility()
        {
        }

        //Mehtode um von einem Server ein jsonFile als String zu bekommen
        //Ergebnis unserer Anfrage mit den Parametern "Anzahl/Count" und "Modus/Mode"
        public string getRequest(string anzahl, string modus)
        {
            //Erstelle den Request
            var request = WebRequest.Create(string.Format(@"https://www.codeyourapp.de/tools/query.php?count={0}&mode={1}", anzahl, modus));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //Wenn StatusCode nicht OK ist, dann geben eine Fehlermeldung zurueck
                if (response.StatusCode != HttpStatusCode.OK)
                    return "Error fetching data. Server returns status code " + response.StatusCode;
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        //Auslesen des Inhalts
                        var content = reader.ReadToEnd();
                        //Bekommen wir keinen oder leeren Inhalt zurueck, geben wir eine Fehlermeldung aus
                        if (string.IsNullOrWhiteSpace(content))
                            return "Response contained empty body";
                        else
                            return content;
                    }
                }
            }
        }

        public List<Zitat> jsonGetZitate(string anzahl, string modus)
        {
            List<Zitat> zitateList = new List<Zitat>();

            //JsonFile als String vom Webserver holen
            var content = getRequest(anzahl, modus);

            //Gibt uns das ZitateArray aus dem Webrequest
            var resultObjects = AllChildren(JObject.Parse(content));
            //Gebe das erste Objekt zurueck welches vom Typ Array ist
            //und im Pfad das Wort "quotes" enthaelt
            var firstResultObject = resultObjects.First(child => child.Type == JTokenType.Array &&
                                                        child.Path.Contains("quotes"));

            //Variante um gezielt einen Token anzusprechen
            //var result = JObject.Parse(content);
            //var diagnostics_count = result.SelectToken("diagnostics.count").ToString();

            //jedes KindObjekt des ZitateArrays in Liste speichern
            foreach (JObject result in firstResultObject)
            {
                Zitat zitat = result.ToObject<Zitat>();
                zitateList.Add(zitat);
            }

            return zitateList;
        }

        //Methode um jeden Knotenpunkt/Token im JsonObjekt zu erreichen
        private static IEnumerable<JToken> AllChildren(JToken jtoken)
        {
            foreach (var child in jtoken.Children())
            {
                yield return child;
                foreach (var childchild in AllChildren(child))
                    yield return childchild;
            }
        }
    }
}
