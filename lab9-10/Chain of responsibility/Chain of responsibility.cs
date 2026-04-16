using System;

namespace ChainOfResponsibilityTask
{
    public class SupportRequest
    {
        public int DifficultyLevel { get; set; }
        public string Description { get; set; }

        public SupportRequest(int difficultyLevel, string description)
        {
            DifficultyLevel = difficultyLevel;
            Description = description;
        }
    }

    abstract class SupportHandler // Handler
    {
        protected SupportHandler successor;

        public void SetSuccessor(SupportHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(SupportRequest request);
    }

    class BasicSupport : SupportHandler // ConcreteHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.DifficultyLevel == 1)
            {
                Console.WriteLine($"[Базовий спецiалiст] вирiшив проблему: {request.Description}");
            }
            else if (successor != null)
            {
                Console.WriteLine($"[Базовий спецiалiст] бракує доступу. Передача запиту далi...");
                successor.HandleRequest(request);
            }
        }
    }

    class MidLevelSupport : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.DifficultyLevel == 2)
            {
                Console.WriteLine($"[Середнiй спецiалiст] вирiшив проблему: {request.Description}");
            }
            else if (successor != null)
            {
                Console.WriteLine($"[Середнiй спецiалiст] бракує компетенцiї. Передача запиту далi...");
                successor.HandleRequest(request);
            }
        }
    }

    class SeniorSupport : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.DifficultyLevel >= 3)
            {
                Console.WriteLine($"[Старший спецiалiст] вирiшив КРИТИЧНУ проблему: {request.Description}");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"[Система] Запит '{request.Description}' неможливо обробити.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Служба пiдтримки ПЗ (Chain of Responsibility) ===\n");

            SupportHandler basic = new BasicSupport();
            SupportHandler mid = new MidLevelSupport();
            SupportHandler senior = new SeniorSupport();

            basic.SetSuccessor(mid);
            mid.SetSuccessor(senior);

            SupportRequest[] requests = {
                new SupportRequest(1, "Скинути пароль користувача"),
                new SupportRequest(2, "Помилка виконання SQL-запиту в базi даних"),
                new SupportRequest(3, "Повне падiння головного сервера"),
                new SupportRequest(1, "Очистити кеш браузера клiєнта")
            };

            foreach (SupportRequest request in requests)
            {
                Console.WriteLine($"\n--- Новий запит: {request.Description} (Рiвень: {request.DifficultyLevel}) ---");
                basic.HandleRequest(request);
            }

            Console.ReadKey();
        }
    }
}