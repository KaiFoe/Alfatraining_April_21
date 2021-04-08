using System;
namespace Zitate
{
    public class Zitat
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public Zitat() { }

        public Zitat(string id, string author, string text)
        {
            Id = id;
            Author = author;
            Text = text;
        }
    }
}
