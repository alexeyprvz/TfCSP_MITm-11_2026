using System;
using System.Collections.Generic;

namespace PrototypeTask
{
    public abstract class Component //Prototype
    {
        public string Manufacturer { get; set; }
        public abstract Component Clone();
    }

    public class Case : Component //ConcretePrototype
    {
        public string FormFactor { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class RAM : Component
    {
        public int CapacityGB { get; set; }
        public int FrequencyMHz { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class HDD : Component
    {
        public int CapacityGB { get; set; }
        public string Type { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class Motherboard : Component
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class CPU : Component
    {
        public int Cores { get; set; }
        public double ClockSpeedGHz { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class VideoAdapter : Component
    {
        public int MemoryGB { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class Monitor : Component
    {
        public string Resolution { get; set; }
        public int RefreshRateHz { get; set; }
        public override Component Clone() => (Component)this.MemberwiseClone();
    }

    public class PersonalComputer
    {
        public string TypeName { get; set; }

        public Case CaseConfig { get; set; }
        public RAM RAMConfig { get; set; }
        public HDD HDDConfig { get; set; }
        public Motherboard MotherboardConfig { get; set; }
        public CPU CPUConfig { get; set; }
        public VideoAdapter VideoConfig { get; set; }
        public Monitor MonitorConfig { get; set; }

        public PersonalComputer Clone()
        {
            PersonalComputer clone = (PersonalComputer)this.MemberwiseClone();

            clone.CaseConfig = (Case)this.CaseConfig?.Clone();
            clone.RAMConfig = (RAM)this.RAMConfig?.Clone();
            clone.HDDConfig = (HDD)this.HDDConfig?.Clone();
            clone.MotherboardConfig = (Motherboard)this.MotherboardConfig?.Clone();
            clone.CPUConfig = (CPU)this.CPUConfig?.Clone();
            clone.VideoConfig = (VideoAdapter)this.VideoConfig?.Clone();
            clone.MonitorConfig = (Monitor)this.MonitorConfig?.Clone();

            return clone;
        }

        public void PrintSpecs()
        {
            Console.WriteLine($"\n=== Конфiгурацiя: {TypeName} ===");
            Console.WriteLine($"Корпус: {CaseConfig.Manufacturer}, Форм-фактор: {CaseConfig.FormFactor}");
            Console.WriteLine($"Мат. плата: {MotherboardConfig.Manufacturer}, Сокет: {MotherboardConfig.Socket}, Чипсет: {MotherboardConfig.Chipset}");
            Console.WriteLine($"Процесор: {CPUConfig.Manufacturer}, Ядер: {CPUConfig.Cores}, Частота: {CPUConfig.ClockSpeedGHz} GHz");
            Console.WriteLine($"ОЗП: {RAMConfig.Manufacturer}, Об'єм: {RAMConfig.CapacityGB} GB, Частота: {RAMConfig.FrequencyMHz} MHz");
            Console.WriteLine($"Вiдеокарта: {VideoConfig.Manufacturer}, Пам'ять: {VideoConfig.MemoryGB} GB");
            Console.WriteLine($"Накопичувач: {HDDConfig.Manufacturer}, Тип: {HDDConfig.Type}, Об'єм: {HDDConfig.CapacityGB} GB");
            Console.WriteLine($"Монiтор: {MonitorConfig.Manufacturer}, Роздiльна здатнiсть: {MonitorConfig.Resolution}, {MonitorConfig.RefreshRateHz} Hz");
            Console.WriteLine("=====================================\n");
        }
    }

    public class PCConfigurator
    {
        private Dictionary<string, PersonalComputer> _prototypes = new Dictionary<string, PersonalComputer>();

        public PCConfigurator()
        {
            InitializePrototypes();
        }

        private void InitializePrototypes()
        {
            _prototypes["Office"] = new PersonalComputer
            {
                TypeName = "Офiсний ПК",
                CaseConfig = new Case { Manufacturer = "Chieftec", FormFactor = "Mini-Tower" },
                MotherboardConfig = new Motherboard { Manufacturer = "ASUS", Socket = "LGA1700", Chipset = "H610" },
                CPUConfig = new CPU { Manufacturer = "Intel", Cores = 4, ClockSpeedGHz = 3.3 },
                RAMConfig = new RAM { Manufacturer = "Crucial", CapacityGB = 8, FrequencyMHz = 2666 },
                VideoConfig = new VideoAdapter { Manufacturer = "Intel (Вбудована)", MemoryGB = 1 },
                HDDConfig = new HDD { Manufacturer = "Kingston", Type = "SSD", CapacityGB = 256 },
                MonitorConfig = new Monitor { Manufacturer = "Dell", Resolution = "1920x1080", RefreshRateHz = 60 }
            };

            _prototypes["Home"] = new PersonalComputer
            {
                TypeName = "Домашнiй ПК",
                CaseConfig = new Case { Manufacturer = "Deepcool", FormFactor = "Midi-Tower" },
                MotherboardConfig = new Motherboard { Manufacturer = "MSI", Socket = "AM4", Chipset = "B550" },
                CPUConfig = new CPU { Manufacturer = "AMD", Cores = 6, ClockSpeedGHz = 3.6 },
                RAMConfig = new RAM { Manufacturer = "Kingston", CapacityGB = 16, FrequencyMHz = 3200 },
                VideoConfig = new VideoAdapter { Manufacturer = "NVIDIA", MemoryGB = 4 },
                HDDConfig = new HDD { Manufacturer = "Western Digital", Type = "SSD", CapacityGB = 512 },
                MonitorConfig = new Monitor { Manufacturer = "LG", Resolution = "1920x1080", RefreshRateHz = 75 }
            };

            _prototypes["Gaming"] = new PersonalComputer
            {
                TypeName = "Iгровий ПК",
                CaseConfig = new Case { Manufacturer = "NZXT", FormFactor = "Midi-Tower" },
                MotherboardConfig = new Motherboard { Manufacturer = "Gigabyte", Socket = "AM5", Chipset = "X670" },
                CPUConfig = new CPU { Manufacturer = "AMD", Cores = 8, ClockSpeedGHz = 4.5 },
                RAMConfig = new RAM { Manufacturer = "G.Skill", CapacityGB = 32, FrequencyMHz = 6000 },
                VideoConfig = new VideoAdapter { Manufacturer = "NVIDIA", MemoryGB = 12 },
                HDDConfig = new HDD { Manufacturer = "Samsung", Type = "NVMe SSD", CapacityGB = 2000 },
                MonitorConfig = new Monitor { Manufacturer = "ASUS ROG", Resolution = "2560x1440", RefreshRateHz = 165 }
            };
        }

        public PersonalComputer CreatePC(string type)
        {
            if (_prototypes.ContainsKey(type))
            {
                return _prototypes[type].Clone();
            }
            throw new ArgumentException("Невідомий тип ПК");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PCConfigurator configurator = new PCConfigurator();

            PersonalComputer myOfficePC = configurator.CreatePC("Office");
            PersonalComputer myGamingPC = configurator.CreatePC("Gaming");
            PersonalComputer myHomePC = configurator.CreatePC("Home");

            myOfficePC.PrintSpecs();
            myGamingPC.PrintSpecs();
            myHomePC.PrintSpecs();

            Console.WriteLine("\n--- Модернiзуємо створений офiсний ПК (додаємо ОЗП та мiняємо монiтор) ---");
            myOfficePC.RAMConfig.CapacityGB = 16;
            myOfficePC.MonitorConfig.Manufacturer = "Samsung";
            myOfficePC.PrintSpecs();

            Console.WriteLine("\n--- Створюємо ще один базовий офiсний ПК з прототипу ---");
            PersonalComputer anotherOfficePC = configurator.CreatePC("Office");
            anotherOfficePC.PrintSpecs();

            Console.ReadKey();
        }
    }
}