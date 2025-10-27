using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityStudentIS
{
    public class Calculator
    {
        private static readonly Dictionary<string, string> Operations = new Dictionary<string, string>
        {
            {"+", "Сложение"},
            {"-", "Вычитание"},
            {"*", "Умножение"},
            {"/", "Деление"},
            {"%", "Остаток от деления"},
            {"^", "Возведение в степень"},
            {"sqrt", "Квадратный корень"},
            {"!", "Факториал"},
            {"log", "Логарифм по основанию 10"},
            {"ln", "Натуральный логарифм"},
            {"sin", "Синус (в радианах)"},
            {"cos", "Косинус (в радианах)"},
            {"tan", "Тангенс (в радианах)"}
        };

        public static void Run()
        {
            Console.WriteLine("=== ПРОДВИНУТЫЙ КАЛЬКУЛЯТОР ===");
            Console.WriteLine("Доступные операции:");
            
            foreach (var op in Operations)
            {
                Console.WriteLine($"  {op.Key} - {op.Value}");
            }
            
            Console.WriteLine("\nСпециальные команды:");
            Console.WriteLine("  'history' - показать историю вычислений");
            Console.WriteLine("  'clear' - очистить историю");
            Console.WriteLine("  'exit' - выход");
            
            List<string> history = new List<string>();
            
            while (true)
            {
                try
                {
                    Console.Write("\nВведите команду или операцию: ");
                    string input = Console.ReadLine().ToLower().Trim();

                    if (input == "exit")
                    {
                        Console.WriteLine("До свидания!");
                        break;
                    }
                    else if (input == "history")
                    {
                        ShowHistory(history);
                        continue;
                    }
                    else if (input == "clear")
                    {
                        history.Clear();
                        Console.WriteLine("История очищена!");
                        continue;
                    }

                    double result = ProcessInput(input, history);
                    string historyEntry = $"{input} = {result}";
                    history.Add(historyEntry);
                    Console.WriteLine($"Результат: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        public static double ProcessInput(string input, List<string> history)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length == 0)
                throw new ArgumentException("Пустой ввод");

            string operation = parts[0];

            // Обработка унарных операций (один аргумент)
            if (operation == "sqrt" || operation == "!" || operation == "log" || operation == "ln" || 
                operation == "sin" || operation == "cos" || operation == "tan")
            {
                if (parts.Length < 2)
                    throw new ArgumentException($"Для операции '{operation}' требуется один аргумент");

                double num = Convert.ToDouble(parts[1]);
                return Calculate(num, 0, operation);
            }

            // Обработка бинарных операций (два аргумента)
            if (parts.Length < 3)
                throw new ArgumentException("Недостаточно аргументов. Формат: операция число1 число2");

            double a = Convert.ToDouble(parts[1]);
            double b = Convert.ToDouble(parts[2]);
            
            return Calculate(a, b, operation);
        }

        public static double Calculate(double a, double b, string operation)
        {
            switch (operation)
            {
                case "+":
                    return Add(a, b);
                case "-":
                    return Subtract(a, b);
                case "*":
                    return Multiply(a, b);
                case "/":
                    return Divide(a, b);
                case "%":
                    return Modulo(a, b);
                case "^":
                    return Power(a, b);
                case "sqrt":
                    return SquareRoot(a);
                case "!":
                    return Factorial((int)a);
                case "log":
                    return Logarithm(a);
                case "ln":
                    return NaturalLogarithm(a);
                case "sin":
                    return Math.Sin(a);
                case "cos":
                    return Math.Cos(a);
                case "tan":
                    return Math.Tan(a);
                default:
                    throw new InvalidOperationException($"Операция '{operation}' не поддерживается");
            }
        }

        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль невозможно");
            return a / b;
        }

        public static double Modulo(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль невозможно");
            return a % b;
        }

        public static double Power(double a, double b)
        {
            return Math.Pow(a, b);
        }

        public static double SquareRoot(double a)
        {
            if (a < 0)
                throw new ArgumentException("Нельзя извлечь корень из отрицательного числа");
            return Math.Sqrt(a);
        }

        public static double Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentException("Факториал отрицательного числа не определен");
            if (n > 20)
                throw new ArgumentException("Слишком большое число для факториала");
            
            long result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }

        public static double Logarithm(double a)
        {
            if (a <= 0)
                throw new ArgumentException("Логарифм определен только для положительных чисел");
            return Math.Log10(a);
        }

        public static double NaturalLogarithm(double a)
        {
            if (a <= 0)
                throw new ArgumentException("Логарифм определен только для положительных чисел");
            return Math.Log(a);
        }

        private static void ShowHistory(List<string> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("История пуста");
                return;
            }

            Console.WriteLine("\n=== ИСТОРИЯ ВЫЧИСЛЕНИЙ ===");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {history[i]}");
            }
        }
    }
}
