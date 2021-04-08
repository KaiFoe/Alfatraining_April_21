using System;
namespace Notizen
{

    //Modelklasse die ein NotizObjekt repraesentiert
    public class Notiz
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; private set; } //Setter ist private, da nicht von außen veraenderbar
        public DateTime EditDate { get; set; }

        public Notiz()
        {
            CreateDate = DateTime.Now.Date; //Gibt nur Datum zurueck
        }

        public Notiz(string title)
        {
            Title = title;
            CreateDate = DateTime.Now.Date; //Gibt nur Datum zurueck
        }
    }
}
