using System;
using System.Text;

namespace PhoneTranslator
{
    static class Phoneword
    {
        //Übersetze Buchstabe in Zahl
        //gebe ansonsten null zurueck
        static int? TranslateNumber(char c)
        {
            /*switch (c)
            {
                case 'A':
                case 'B':
                case 'C':
                    return 2;
            }*/

            if ("ABC".Contains(c))
                return 2;
            else if ("DEF".Contains(c))
                return 3;
            else if ("GHI".Contains(c))
                return 4;
            else if ("JKL".Contains(c))
                return 5;
            else if ("MNO".Contains(c))
                return 6;
            else if ("PQRS".Contains(c))
                return 7;
            else if ("TUV".Contains(c))
                return 8;
            else if ("WXYZ".Contains(c))
                return 9;

           return null;
        }

        public static string ToNumber(string raw)
        {
            //wenn string null oder leer ist, dann geben wir einen leeren String zur|ck
            if (string.IsNullOrEmpty(raw))
                return "";
            else
            {
                //Alle Buchstaben in Großbuchstaben umwandeln
                raw = raw.ToUpperInvariant();
            }

            //Hilfsvariable um umgewandelte Zeichen zusammenzusetzen
            var newNumber = new StringBuilder();

            foreach (var c in raw)
            {
                //wenn das Zeichen ein Leerzeichen, ein - oder eine Zahl ist, geben wir das Zeichen
                //unveraendert zurueck
                if (" -0123456789".Contains(c))
                    newNumber.Append(c);
                else
                {
                    //wandle Zeichen (A-Z) in Zahl
                    var result = TranslateNumber(c);
                    //wenn das Ergebnis nicht null, fuege Zahl der Nummer hinzu
                    if (result != null)
                        newNumber.Append(result);
                }
            }

            //wandle Zeichenkette in einen String um und gebe diesen zurueck
            return newNumber.ToString();

        }
    }
}
