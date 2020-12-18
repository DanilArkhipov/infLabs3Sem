using System;

namespace Prac4.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Relise_Date { get; set; }

        public Movies(string name, string genre,string author,string reliseDate,int id = 0)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Author = author;
            Relise_Date = reliseDate;
        }

        public Movies(){}
    }
}