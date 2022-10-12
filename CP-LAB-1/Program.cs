using System;
using System.IO;
using System.Numerics;
using System.Threading;

namespace CP_LAB_1
{
    class Program
    {
        const string OutputFile = "OUTPUT.TXT";
        const string InputFile = "INPUT.TXT";

        private static void Main(string[] args)
        {
            if (!File.Exists(InputFile))
            {
                Console.WriteLine($"{InputFile} не найден.");
                return;
            }

            var fileLines = File.ReadAllLines(InputFile);

            if (fileLines.Length == 0)
            {
                Console.WriteLine($"{InputFile} пуст.");
                return;
            }

            if (int.TryParse(fileLines[0], out int value) && value >= 1)
            {
                if(value > 10000)
                {
                    Console.WriteLine("значение должно быть меньше 10000. Ошибка");
                    return;
                }

                Console.WriteLine($"Значение с файла: {value}.");
                Console.WriteLine("...\n");

                var res = GetResult(value);

                if (res == 0)
                {
                    Console.WriteLine($"Ошибка попробуйте снова");
                }

                Console.WriteLine($"Ответ = {res}. Записан в файл {OutputFile}.");
                File.WriteAllText(OutputFile, $"{res}");
            }
            else
            {
                Console.WriteLine($"{InputFile} значение не число.");
            }

            Console.WriteLine("\nНажмите что-то чтоб закончить...");
            Console.ReadKey();
        }

        private static int GetResult(int n)
        {
            if(n == 0)
                return 0;
            return GetSn(n);
        }

        private static int GetSn(int n)
        {
            if(n <= 0)
            {
                return 0;
            }
            else
            {
                return GetSn(n - 1) + n * (n + 1) + GetNPlus(n);
            }
        }

        private static int GetNPlus(int number)
        {
            if(number <= 1)
            {
                return 1;
            }
            else
            {
                return number + GetNPlus(number - 1);
            }
        }
    }
}