using System;
using SQLite;

namespace ToDoListe
{
    public class ToDo
    {
        //Erweiterungsparamter fuer die SQLite-DB
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set;}
        public DateTime CreateDate { get; set; }
        public bool IsDone { get; set; }
        public DateTime EditDate { get; set; }

        public ToDo()
        {

        }

        public ToDo(string name)
        {
            Name = name;
            CreateDate = DateTime.Now.Date;
            EditDate = DateTime.Now;
            IsDone = false;
        }
    }
}
