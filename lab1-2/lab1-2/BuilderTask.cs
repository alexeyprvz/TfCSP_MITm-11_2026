using System;

namespace MailMessageBuilderApp
{
    public class MailMessage
    {
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void Show()
        {
            Console.WriteLine("--- Електронний лист ---");
            Console.WriteLine($"Вiд:   {Sender}");
            Console.WriteLine($"Кому:  {Receiver}");
            Console.WriteLine($"Тема:  {Subject}");
            Console.WriteLine($"Вмiст: {Body}");
            Console.WriteLine("------------------------\n");
        }
    }

    public abstract class Builder
    {
        public abstract Builder SetSender(string Sender);
        public abstract Builder SetReceiver(string Receiver);
        public abstract Builder SetSubject(string subject);
        public abstract Builder SetBody(string body);
        public abstract MailMessage GetResult();
    }

    public class MailMessageBuilder : Builder
    {
        private MailMessage _email = new MailMessage();

        public override Builder SetSender(string Sender)
        {
            _email.Sender = Sender;
            return this;
        }

        public override Builder SetReceiver(string Receiver)
        {
            _email.Receiver = Receiver;
            return this;
        }

        public override Builder SetSubject(string subject)
        {
            _email.Subject = subject;
            return this;
        }

        public override Builder SetBody(string body)
        {
            _email.Body = body;
            return this;
        }

        public override MailMessage GetResult()
        {
            return _email;
        }
    }

    public class Director
    {
        public void ConstructSupportEmail(Builder builder)
        {
            builder.SetSender("support@gmail.com")
                   .SetReceiver("client@gmail.com")
                   .SetSubject("Технiчнi роботи")
                   .SetBody("Повiдомляємо про плановi роботи на мережi.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            Builder builder = new MailMessageBuilder();

            director.ConstructSupportEmail(builder);
            MailMessage email1 = builder.GetResult();
            email1.Show();

            Console.ReadKey();
        }
    }
}