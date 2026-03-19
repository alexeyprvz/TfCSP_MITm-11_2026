using System;
using System.Collections.Generic;

namespace CompositeTask
{
    public interface IShape // Component
    {
        void Add(IShape shape);
        void Remove(IShape shape);
        void Draw(int depth);
    }

    public class Circle : IShape // Leaf
    {
        public void Add(IShape shape)
        {
            Console.WriteLine("Неможливо додати до простого Кола");
        }

        public void Remove(IShape shape)
        {
            Console.WriteLine("Неможливо видалити з простого Кола");
        }

        public void Draw(int depth)
        { 
            Console.WriteLine(new String('-', depth) + " Коло");
        }
    }

    public class Square : IShape
    {
        public void Add(IShape shape)
        {
            Console.WriteLine("Неможливо додати до простого Квадрата");
        }

        public void Remove(IShape shape)
        {
            Console.WriteLine("Неможливо видалити з простого Квадрата");
        }

        public void Draw(int depth)
        {
            Console.WriteLine(new String('-', depth) + " Квадрат");
        }
    }

    public class CompositeShape : IShape // Composite
    {
        private List<IShape> _shapes = new List<IShape>();

        public void Add(IShape shape)
        {
            _shapes.Add(shape);
        }

        public void Remove(IShape shape)
        {
            _shapes.Remove(shape);
        }

        public void Draw(int depth)
        {
            Console.WriteLine(new String('-', depth) + " Складена фігура:");
            foreach (var shape in _shapes)
            {
                shape.Draw(depth + 2);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== 1. Шаблон Composite (Прозорий підхід) ===\n");

            IShape rootShape = new CompositeShape();
            IShape circle = new Circle();

            rootShape.Add(circle);

            IShape group = new CompositeShape();
            group.Add(new Square());
            group.Add(new Circle());

            rootShape.Add(group);

            rootShape.Draw(1);

            Console.WriteLine("\n--- Перевірка реакції простої фігури (Leaf) ---");
            circle.Add(new Square());

            Console.ReadKey();
        }
    }
}