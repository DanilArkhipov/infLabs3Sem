using System;

namespace Prac4.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Janr { get; set; }
        public string Author { get; set; }
        public string ReliseDate { get; set; }

        public Movie(Guid id,string name, string janr,string author,string reliseDate)
        {
            Id = id;
            Name = name;
            Janr = janr;
            Author = author;
            ReliseDate = reliseDate;
        }
    }
}