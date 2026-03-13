using System;
using System.Collections.Generic;
using System.Threading;

namespace ProxyPatternTask
{
    public interface IWebServer //Subject
    {
        string GetPage(string url);
    }

    public class RealWebServer : IWebServer // RealSubject
    {
        public string GetPage(string url)
        {
            Console.WriteLine($"[Мережа] Встановлення з'єднання та завантаження сторінки {url}...");

            Thread.Sleep(2000);

            return $"<html>Вміст веб-сторінки {url}</html>";
        }
    }

    public class ProxyWebServer : IWebServer // Proxy
    {
        private RealWebServer _realServer;

        private Dictionary<string, string> _pageCache = new Dictionary<string, string>(); // Cache

        public string GetPage(string url)
        {
            if (_pageCache.ContainsKey(url))
            {
                Console.WriteLine($"[Кеш Проксі] Сторінку {url} знайдено в локальному кеші. Миттєва віддача.");
                return _pageCache[url];
            }

            if (_realServer == null)
            {
                _realServer = new RealWebServer();
            }

            Console.WriteLine($"[Проксі] Сторінки {url} немає в кеші. Перенаправлення запиту до реального сервера...");

            string pageContent = _realServer.GetPage(url);

            _pageCache[url] = pageContent;

            return pageContent;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Емулятор кешуючого проксі-сервера ===\n");

            IWebServer proxy = new ProxyWebServer();

            Console.WriteLine("--- Клієнт запитує www.knu.ua ---");
            Console.WriteLine($"Результат: {proxy.GetPage("www.knu.ua")}\n");

            Console.ReadKey();

            Console.WriteLine("--- Клієнт знову запитує www.knu.ua ---");
            Console.WriteLine($"Результат: {proxy.GetPage("www.knu.ua")}\n");

            Console.ReadKey();

            Console.WriteLine("--- Клієнт запитує www.google.com ---");
            Console.WriteLine($"Результат: {proxy.GetPage("www.google.com")}\n");

            Console.ReadKey();
        }
    }
}