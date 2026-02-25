using System;

namespace FactoryMethodTask
{
    // Product - загальний інтерфейс файлу
    public abstract class CustomFile
    {
        public abstract void Open();
    }

    // ConcreteProductA - Текстовий файл
    public class TextFile : CustomFile
    {
        public override void Open()
        {
            Console.WriteLine("Вiдкрито текстовий файл (.txt). Можна читати текст.");
        }
    }

    // ConcreteProductB - Графічний файл
    public class GraphicFile : CustomFile
    {
        public override void Open()
        {
            Console.WriteLine("Вiдкрито графiчний файл (.png / .jpg). Можна переглядати зображення.");
        }
    }

    // Creator - Абстрактна фабрика
    public abstract class FileManager
    {
        // Той самий Factory Method
        public abstract CustomFile CreateFile();
    }

    // ConcreteCreatorA
    public class TextFileManager : FileManager
    {
        public override CustomFile CreateFile()
        {
            return new TextFile();
        }
    }

    // ConcreteCreatorB
    public class GraphicFileManager : FileManager
    {
        public override CustomFile CreateFile()
        {
            return new GraphicFile();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Який файл ви хочете створити?");
            Console.WriteLine("1 - Текстовий файл");
            Console.WriteLine("2 - Графiчний файл");
            Console.Write("Ваш вибiр: ");

            string choice = Console.ReadLine();
            FileManager manager = null;

            // Вибір фабрики під час роботи програми
            if (choice == "1")
            {
                manager = new TextFileManager();
            }
            else if (choice == "2")
            {
                manager = new GraphicFileManager();
            }
            else
            {
                Console.WriteLine("Невiрний вибiр.");
                return;
            }

            // Створення та використання об'єкта через загальний інтерфейс
            CustomFile file = manager.CreateFile();
            file.Open();

            Console.ReadKey();
        }
    }
}