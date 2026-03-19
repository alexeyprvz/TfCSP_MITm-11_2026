using System;

namespace AdapterPatternTask
{
    public class MessageSender // Target
    {
        public virtual void Send(string message, string recipient)
        {
            Console.WriteLine($"[Електронна пошта] Відправлено на {recipient}: {message}");
        }
    }

    public class SMSsender // Adaptees
    {
        public void SendSMS(string message, string phoneNumber)
        {
            Console.WriteLine($"[SMS Сповіщення] Відправлено на номер {phoneNumber}: {message}");
        }
    }

    public class TelegramSender
    {
        public void SendTelegram(string message, string username)
        {
            Console.WriteLine($"[Telegram Сповіщення] Відправлено користувачу {username}: {message}");
        }
    }

    public class SmsAdapter : MessageSender // Adapters
    {
        private SMSsender _smsSender = new SMSsender();

        public override void Send(string message, string recipient)
        {
            _smsSender.SendSMS(message, recipient);
        }
    }

    public class TelegramAdapter : MessageSender
    {
        private TelegramSender _telegramSender = new TelegramSender();

        public override void Send(string message, string recipient)
        {
            _telegramSender.SendTelegram(message, recipient);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Робота системи сповіщень (Шаблон Adapter) ===\n");

            MessageSender emailSender = new MessageSender();
            emailSender.Send("Заявка на пiдключення прийнята.", "client@domain.com");

            MessageSender smsSender = new SmsAdapter();
            smsSender.Send("Аварiя на комутаторi агрегацiї усунена.", "+380501234567");

            MessageSender telegramSender = new TelegramAdapter();
            telegramSender.Send("Оновлення прошивки MikroTik завершено успiшно.", "@client_username");

            Console.ReadKey();
        }
    }
}