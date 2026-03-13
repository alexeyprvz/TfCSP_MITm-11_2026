using System;
using System.Threading;

namespace SingletonTask
{
    public sealed class Service
    {
        private static readonly Lazy<Service> _instance = new Lazy<Service>(() => new Service());

        private bool _isWorking = false;

        public static Service Instance
        {
            get { return _instance.Value; }
        }

        private Service() { }

        public string GetStatus()
        {
            return _isWorking ? "Working" : "Idle";
        }

        public void DoWork(string message)
        {
            _isWorking = true;
            Console.WriteLine($"[Поточний статус: {GetStatus()}]");

            Console.WriteLine($"-> Виконується завдання: {message}");

            Thread.Sleep(1500);

            _isWorking = false;
            Console.WriteLine($"[Поточний статус: {GetStatus()}] Завдання завершено.\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрацiя шаблону Singleton ===\n");

            Service myService = Service.Instance;

            Console.WriteLine($"Статус до початку роботи: {myService.GetStatus()}\n");

            myService.DoWork("Дiагностика зникнення лiнку на комутаторi агрегацiї");

            Service anotherServiceReference = Service.Instance;

            if (myService == anotherServiceReference)
            {
                Console.WriteLine("Перевiрка: myService та anotherServiceReference — це один i той самий об'єкт!\n");
            }

            anotherServiceReference.DoWork("Оновлення прошивки на клiєнтському MikroTik");

            Console.WriteLine($"Статус пiсля завершення всiх робiт: {myService.GetStatus()}");

            Console.ReadKey();
        }
    }
}