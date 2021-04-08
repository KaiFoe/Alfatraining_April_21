using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace ToDoListe
{
    public class DBHelper
    {
        static string DBName = "ToDoList.db3";
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DBName);

        SQLiteConnection db;

        //Erstellen der Datenbnak
        public void createDB()
        {
            try
            {
                db = new SQLiteConnection(dbPath);
                db.CreateTable<ToDo>();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Fehler beim erstellen der Datenbank: " + ex);
            }
            finally
            {
                db.Close();
            }
        }

        //Hinzuf|gen eines ToDoObjektes
        public void addToDo(ToDo todo)
        {
            db = new SQLiteConnection(dbPath);
            db.Insert(todo);
            db.Close();
        }

        //Loeschen eines TaskObjekts
        public void delete(ToDo toDo)
        {
            db = new SQLiteConnection(dbPath);
            db.Delete(toDo);
            //db.Query<ToDo>("DELETE * FROM ToDo WHERE _id = ?", toDo.Id);
            db.Close();
        }

        public void deleteAllToDos()
        {
            db = new SQLiteConnection(dbPath);
            db.DeleteAll<ToDo>();
            db.Close();
        }

        public List<ToDo> getAllToDos()
        {
            db = new SQLiteConnection(dbPath);
            List<ToDo> toDoList = db.Query<ToDo>("SELECT * FROM ToDo");
            db.Close();
            return toDoList;
        }

        public void updateToDo(ToDo toDo)
        {
            db = new SQLiteConnection(dbPath);
            db.Update(toDo);
            db.Close();
        }

        /*public int GetDatabaseVersion()
        {
            db = new SQLiteConnection(dbPath);
            int dbVersion = db.Execute("PRAGMA user_version");
            Console.WriteLine("DB-Version : " + dbVersion);
            db.Close();
            return dbVersion;

        }*/
    }
}
