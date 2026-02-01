using System;
using System.Collections.Generic;
using System.IO;

namespace Module02_Homework
{
    // ===== DRY: 1 метод вместо 3 одинаковых =====
    public enum LogLevel
    {
        Error,
        Warning,
        Info
    }

    public class Logger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine($"{level.ToString().ToUpper()}: {message}");
        }

        // Эти методы можно оставить ради читаемости,
        // но дублирования уже нет — внутри общий метод.
        public void LogError(string message) => Log(LogLevel.Error, message);
        public void LogWarning(string message) => Log(LogLevel.Warning, message);
        public void LogInfo(string message) => Log(LogLevel.Info, message);
    }

    // ===== DRY: общие конфигурационные настройки =====
    public static class AppConfig
    {
        public static string ConnectionString =>
            "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
    }

    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            // Тут должна быть логика подключения.
            Console.WriteLine($"[DB] Connecting using: {_connectionString}");
        }
    }

    public class LoggingService
    {
        private readonly string _connectionString;

        public LoggingService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void LogToDatabase(string message)
        {
            // Тут должна быть логика записи лога в БД.
            Console.WriteLine($"[DB-LOG] ({_connectionString}) {message}");
        }
    }

    // ===== KISS: меньше вложенности, ранний выход =====
    public class NumberProcessor
    {
        public void ProcessNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return;

            foreach (int number in numbers)
            {
                if (number > 0)
                    Console.WriteLine(number);
            }
        }

        // ===== KISS: без лишнего LINQ (просто и понятно) =====
        public void PrintPositiveNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return;

            List<int> positive = new List<int>();

            foreach (int n in numbers)
            {
                if (n > 0)
                    positive.Add(n);
            }

            positive.Sort();

            foreach (int n in positive)
            {
                Console.WriteLine(n);
            }
        }

        // ===== KISS: не ловим исключение там, где можно проверить =====
        public int Divide(int a, int b)
        {
            if (b == 0)
                return 0;

            return a / b;
        }
    }

    // ===== YAGNI: User — только данные, без лишних методов =====
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public User(string name, string email, string address)
        {
            Name = name;
            Email = email;
            Address = address;
        }
    }

    // ===== YAGNI: FileReader без лишних настроек буфера =====
    public class FileReader
    {
        public string ReadFile(string filePath)
        {
            // Минимально нужно: просто прочитать файл
            return File.ReadAllText(filePath);
        }
    }

    // ===== YAGNI: ReportGenerator без 3 форматов, один минимальный отчёт =====
    public class ReportGenerator
    {
        public void GenerateTextReport(string title, string body)
        {
            Console.WriteLine("=== REPORT ===");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine(body);
            Console.WriteLine("==============");
        }
    }

    class Program
    {
        static void Main()
        {
            // --- DRY: Logger ---
            Logger logger = new Logger();
            logger.LogError("Something went wrong");
            logger.LogWarning("This is a warning");
            logger.LogInfo("Everything is OK");

            Console.WriteLine();

            // --- DRY: common config ---
            DatabaseService db = new DatabaseService(AppConfig.ConnectionString);
            LoggingService dbLog = new LoggingService(AppConfig.ConnectionString);

            db.Connect();
            dbLog.LogToDatabase("User created");

            Console.WriteLine();

            // --- KISS examples ---
            NumberProcessor processor = new NumberProcessor();

            int[] numbers = { -2, 0, 5, 3, -1, 10 };
            Console.WriteLine("Positive numbers (ProcessNumbers):");
            processor.ProcessNumbers(numbers);

            Console.WriteLine("\nPositive numbers sorted (PrintPositiveNumbers):");
            processor.PrintPositiveNumbers(numbers);

            Console.WriteLine($"\nDivide 10 / 2 = {processor.Divide(10, 2)}");
            Console.WriteLine($"Divide 10 / 0 = {processor.Divide(10, 0)}");

            Console.WriteLine();

            // --- YAGNI examples ---
            User user = new User("Sara", "sara@mail.com", "Almaty");
            Console.WriteLine($"User: {user.Name}, {user.Email}, {user.Address}");

            ReportGenerator report = new ReportGenerator();
            report.GenerateTextReport("User info", $"Name: {user.Name}\nEmail: {user.Email}\nAddress: {user.Address}");

            // FileReader пример можно протестить, если реально есть файл:
            // FileReader reader = new FileReader();
            // Console.WriteLine(reader.ReadFile("test.txt"));
        }
    }
}
