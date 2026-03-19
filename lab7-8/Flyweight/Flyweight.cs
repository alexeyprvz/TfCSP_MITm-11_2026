using System;
using System.Collections.Generic;

namespace FlyweightTask
{
    public class BookFlyweight // Flyweight
    {
        public string Title { get; private set; }
        public string Author { get; private set; }

        public BookFlyweight(string title, string author)
        {
            Title = title;
            Author = author;
            Console.WriteLine($"[Фабрика] Створено новий еталон книги: '{Title}' ({Author})");
        }
    }

    public class BookFlyweightFactory // Flyweight Factory
    {
        private Dictionary<string, BookFlyweight> _flyweights = new Dictionary<string, BookFlyweight>();

        public BookFlyweight GetBookFlyweight(string title, string author)
        {
            string key = title + "_" + author;
            if (!_flyweights.ContainsKey(key))
            {
                _flyweights[key] = new BookFlyweight(title, author);
            }
            return _flyweights[key];
        }
    }

    public class Book
    {
        private BookFlyweight _flyweight;
        public string InventoryNumber { get; set; }

        public Book(BookFlyweight flyweight, string inventoryNumber)
        {
            _flyweight = flyweight;
            InventoryNumber = inventoryNumber;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Інвентарний №: {InventoryNumber} | Книга: '{_flyweight.Title}', Автор: {_flyweight.Author}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Шаблон Flyweight ===");
            BookFlyweightFactory factory = new BookFlyweightFactory();
            List<Book> library = new List<Book>();

            BookFlyweight hpFlyweight = factory.GetBookFlyweight("Гаррі Поттер", "Дж. Роулінг");

            library.Add(new Book(hpFlyweight, "INV-001"));
            library.Add(new Book(hpFlyweight, "INV-002"));
            library.Add(new Book(hpFlyweight, "INV-003"));

            BookFlyweight lotrFlyweight = factory.GetBookFlyweight("Володар Перснів", "Дж.Р.Р. Толкін");
            library.Add(new Book(lotrFlyweight, "INV-004"));

            Console.WriteLine("\nСписок книг у книгарні:");
            foreach (var book in library)
            {
                book.DisplayInfo();
            }

            Console.ReadKey();
        }
    }
}