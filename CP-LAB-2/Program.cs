using System;
using System.IO;
using System.Numerics;
using System.Threading;

namespace CP_LAB_2
{
    class Program
    {
        const string OutputFile = "OUTPUT.TXT";
        const string InputFile = "INPUT.TXT";
        static int[] firstRaw;
        static int[] SecondRaw;
        static int[] ThirdRaw;
        static int FourthRaw;
        static int[] FifthRaw;


        private static void Main(string[] args)
        {
            var temp = GetSutibleDaysAmount(3, 31, 7, 7,new int[] { 7}, new int[] {1, 9});

            if (!File.Exists(InputFile))
            {
                Console.WriteLine($"{InputFile} не найден!");
                return;
            }

            var fileLines = File.ReadAllLines(InputFile);

            if (fileLines.Length != 5)
            {
                Console.WriteLine($"{InputFile} не подходит по колву строк! Рекомендация 5 строк.");
                return;
            }

            try
            {
                firstRaw = fileLines[0].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                SecondRaw = fileLines[1].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                ThirdRaw = fileLines[2].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                FifthRaw = fileLines[4].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            }
            catch(Exception e)
            {
                Console.WriteLine("В файле есть не число, ошибка!");
                return;
            }

            if (int.TryParse(fileLines[3], out int number))
            {
                FourthRaw = number;
            }
            else 
            { 
                Console.WriteLine("В файле есть не число, ошибка!");
                return;
            }

            if (firstRaw.Length != 2 && SecondRaw.Length != 3)
            {
                Console.WriteLine("Неверный формат 1й или 2й строки");
                return;
            }

            var res = GetResult();

            if (res == -1)
            {
                Console.WriteLine($"Ошибка в значениях попробуйте снова");
                return;
            }

            Console.WriteLine($"Ответ = {res}. Записан в файл {OutputFile}.");
            File.WriteAllText(OutputFile, $"{res}");            

            Console.WriteLine("\nНажмите что-то чтобы закончить...");
            Console.ReadKey();
        }

        static private int GetResult()
        {
            int n, k, w, s;
            int dw, dm;

            n = firstRaw[0];
            k = firstRaw[1];

            if (!(1 <= k && k <= n && n <= 100000))
                return -1;

            w = SecondRaw[0];
            dw = SecondRaw[1];
            s = SecondRaw[2];

            if (!(1 <= s && s <= w && w <= n) && !(0 <= dw && dw <= w))
                return -1;
                
            if(ThirdRaw.Length != dw)
                return -1;

            dm = FourthRaw;

            if (FifthRaw.Length != dm)
                return -1;

            return GetSutibleDaysAmount(k,n,w,s,ThirdRaw,FifthRaw);
        }

        static private int GetSutibleDaysAmount(int k,int n,int w,int s,int[] dw,int[] dm)
        {
            var result = 0;
            var availableDays = 0;

            for(int i = 1; i<=n; i++)
            {
                if (w < s)
                    s = 1;
                if (dw.Contains(s))
                {
                    result += GetPartResult(availableDays, k);
                    availableDays = 0;
                    s++;
                    continue;
                }

                if (dm.Contains(i))
                {
                    result += GetPartResult(availableDays, k);
                    availableDays = 0;
                    s++;
                    continue;
                }

                availableDays++;
                if (w == s)
                    s = 0;
                s++;
            }

            return result;
        }

        static private int GetPartResult(int days, int k)
        {
            if (days < k)
                return 0;

            return days - k + 1;
        }
    }
}