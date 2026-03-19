using System;

namespace DecoratorTask
{
    public abstract class IceCream // Component
    {
        public abstract string GetDescription();
        public abstract double GetCost();
    }

    public class BaseIceCream : IceCream // ConcreteComponent
    {
        public override string GetDescription() => "Вершкове морозиво";
        public override double GetCost() => 50.0;
    }

    public abstract class IceCreamDecorator : IceCream // Decorator
    {
        protected IceCream _iceCream;

        public IceCreamDecorator(IceCream iceCream)
        {
            _iceCream = iceCream;
        }

        public override string GetDescription() => _iceCream.GetDescription();
        public override double GetCost() => _iceCream.GetCost();
    }

    public class ChocolateDecorator : IceCreamDecorator // ConcreteDecorator
    {
        public ChocolateDecorator(IceCream iceCream) : base(iceCream) { }

        public override string GetDescription() => _iceCream.GetDescription() + ", з шоколадом";
        public override double GetCost() => _iceCream.GetCost() + 15.0;
    }

    public class CaramelDecorator : IceCreamDecorator
    {
        public CaramelDecorator(IceCream iceCream) : base(iceCream) { }

        public override string GetDescription() => _iceCream.GetDescription() + ", з карамеллю";
        public override double GetCost() => _iceCream.GetCost() + 10.0;
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Шаблон Decorator ===");
            IceCream myIceCream = new BaseIceCream();
            myIceCream = new ChocolateDecorator(myIceCream);
            myIceCream = new CaramelDecorator(myIceCream);

            Console.WriteLine($"Склад: {myIceCream.GetDescription()}");
            Console.WriteLine($"Вартість: {myIceCream.GetCost()} грн\n");
            Console.ReadKey();
        }
    }
}