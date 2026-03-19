using System;

namespace FacadeTask
{
    public class CPU
    {
        public void Freeze() => Console.WriteLine("CPU: Заморожування процесів...");
        public void Jump(long position) => Console.WriteLine($"CPU: Перехід за адресою {position}...");
        public void Execute() => Console.WriteLine("CPU: Виконання інструкцій...");
    }

    public class Memory
    {
        public void Load(long position, string data) => Console.WriteLine($"Memory: Завантаження даних '{data}' в позицію {position}...");
    }

    public class HardDrive
    {
        public string Read(long lba, int size) => "ОС Windows";
    }

    public class ComputerFacade
    {
        private CPU _cpu;
        private Memory _memory;
        private HardDrive _hardDrive;

        public ComputerFacade()
        {
            _cpu = new CPU();
            _memory = new Memory();
            _hardDrive = new HardDrive();
        }

        public void turnOn()
        {
            Console.WriteLine("--- Початок запуску комп'ютера ---");
            _cpu.Freeze();
            string bootSector = _hardDrive.Read(0, 1024);
            _memory.Load(0, bootSector);
            _cpu.Jump(0);
            _cpu.Execute();
            Console.WriteLine("--- Комп'ютер успішно запущено ---");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Шаблон Facade ===");
            ComputerFacade computer = new ComputerFacade();
            computer.turnOn();

            Console.ReadKey();
        }
    }
}