using System;

namespace UniversityStudentIS
{
    public class Calculator
    {
        public static void Run()
        {
            Console.WriteLine("=== Простой Калькулятор ===");
            Console.WriteLine("Доступные операции: +, -, *, /");
            
            while (true)
            {
                try
                {
                    Console.Write("Введите первое число: ");
                    double num1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите операцию (+, -, *, /): ");
                    string operation = Console.ReadLine();

                    Console.Write("Введите второе число: ");
                    double num2 = Convert.ToDouble(Console.ReadLine());

                    double result = Calculate(num1, num2, operation);
                    Console.WriteLine($"Результат: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                Console.Write("Продолжить? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                    break;
            }
        }

        public static double Calculate(double a, double b, string operation)
        {
            switch (operation)
            {
                case "+":
                    return Add(a, b);
                default:
                    throw new InvalidOperationException($"Операция '{operation}' пока не поддерживается");
            }
        }

        public static double Add(double a, double b)
        {
            return a + b;
        }
    }
}
