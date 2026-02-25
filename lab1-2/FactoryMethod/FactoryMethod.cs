using System;
using System.IO;

namespace FactoryMethodApp
{
    abstract class File
    {
        public abstract void CreatelFile(string fileName);
    }

    class TextFileProduct : File
    {
        public override void CreatelFile(string fileName)
        {
            string fullPath = fileName + ".txt";
            System.IO.File.WriteAllText(fullPath, "Це автоматично згенерований текстовий файл.");
            Console.WriteLine($"-> Cтворено текстовий файл за шляхом:\n{Path.GetFullPath(fullPath)}");
        }
    }

    class GraphicFileProduct : File
    {
        public override void CreatelFile(string fileName)
        {
            string fullPath = fileName + ".png";
            System.IO.File.Create(fullPath).Close();
            Console.WriteLine($"-> Cтворено графiчний файл за шляхом:\n{Path.GetFullPath(fullPath)}");
        }
    }

    abstract class Creator
    {
        public abstract File FactoryMethod();
    }

    class TextFileCreator : Creator
    {
        public override File FactoryMethod()
        {
            return new TextFileProduct();
        }
    }

    class GraphicFileCreator : Creator
    {
        public override File FactoryMethod()
        {
            return new GraphicFileProduct();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Шаблон Factory Method ===");
            Console.WriteLine("Який тип файлу ви хочете фiзично створити?");
            Console.WriteLine("1 - Текстовий файл (.txt)");
            Console.WriteLine("2 - Графiчний файл (.png)");
            Console.Write("Ваш вибiр: ");

            string choice = Console.ReadLine();
            Creator creator = null;

            if (choice == "1")
            {
                creator = new TextFileCreator();
            }
            else if (choice == "2")
            {
                creator = new GraphicFileCreator();
            }
            else
            {
                Console.WriteLine("Невiрний вибiр!");
                return;
            }

            Console.Write("Введiть iм'я для нового файлу (без розширення): ");
            string fileName = Console.ReadLine();

            File fileObject = creator.FactoryMethod();
            fileObject.CreatelFile(fileName);

            Console.ReadKey();
        }
    }
}