using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Book
    {
        string name, authors, genre, PbHouse, binding, source, comment, isbn;
        public string Name 
        {
            get => name; 
            set
            {
                if (!string.IsNullOrEmpty(value))
                    name = value;
            }
        }
        public string Authors
        {
            get => authors;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    authors = value;
            }
        }
        public string Genre
        {
            get => genre;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    genre = value;
            }
        }
        public string PublishingHouse
        {
            get => PbHouse;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    PbHouse = value;
            }
        }
        public string Binding
        {
            get => binding;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    binding = value;
            }
        }
        public string Source
        {
            get => source;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    source = value;
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    comment = value;
            }
        }
        public string ISBN
        {
            get => isbn;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    isbn = value;
            }
        }
        int year, mark;
        public int Year 
        {
            get => year;
            set
            {
                if (value >= 300 && value <= DateTime.Now.Year)
                    year = value;
            }
        }
        public int Mark
        {
            get => mark; 
            set
            {
                if (value >= 0 && value <= 10)
                    mark = value;
            }
        }
        DateTime got, read;
        public DateTime Got
        {
            get => got;
            set
            {
                if (value.Year >= year)
                    got = value;
            }
        }
        public DateTime Read 
        { 
            get => read;
            set
            {
                if (value >= got)
                    read = value;
            }
        }
        public Book(string name, string authors, string genre, string pb, string binding, string source, string comment, string isbn, int year, int mark, DateTime got, DateTime read)
        {
            this.Name = name;
            this.Authors = authors;
            this.Genre = genre;
            this.PublishingHouse = pb;
            this.Binding = binding;
            this.Source = source;
            this.Comment = comment;
            this.ISBN = isbn;
            this.Year = year;
            this.Mark = mark;
            this.Got = got;
            this.Read = read;
        }
        public override string ToString()
        {
            return name + $", ({Authors}); " + year.ToString() + $" - {ISBN}";
        }
    }
}
