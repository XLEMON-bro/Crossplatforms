using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_LAB_5_LIB.LABs
{
    public class LAB2 : ILabWorker
    {
        static int[] firstRaw;
        static int[] SecondRaw;
        static int[] ThirdRaw;
        static int FourthRaw;
        static int[] FifthRaw;

        public async Task<string> GetOutputForLab(string Input)
        {
            return await Task.Run(() =>
            {
                return GetOutput(Input);
            });
        }

        private string GetOutput(string Input)
        {
            var strMas = Input.Split("\r\n");

            if (strMas.Length != 5)
            {
                return "Не подходит по колву строк! Рекомендация 5 строк!";
            }

            try
            {
                firstRaw = strMas[0].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                SecondRaw = strMas[1].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                ThirdRaw = strMas[2].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
                FifthRaw = strMas[4].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            }
            catch (Exception e)
            {
                return "Есть не число, ошибка!";
            }

            if (int.TryParse(strMas[3], out int number))
            {
                FourthRaw = number;
            }
            else
            {
                return "Есть не число, ошибка!";
            }

            if (firstRaw.Length != 2 && SecondRaw.Length != 3)
            {
                return "Неверный формат 1й или 2й строки!";
            }

            var res = GetResult();

            if (res == -1)
            {
                return "Ошибка в значениях попробуйте снова!";
            }

            return res.ToString();
        }

        private int GetResult()
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

            if (ThirdRaw.Length != dw)
                return -1;

            dm = FourthRaw;

            if (FifthRaw.Length != dm)
                return -1;

            return GetSutibleDaysAmount(k, n, w, s, ThirdRaw, FifthRaw);
        }

        private int GetSutibleDaysAmount(int k, int n, int w, int s, int[] dw, int[] dm)
        {
            var result = 0;
            var availableDays = 0;

            for (int i = 1; i <= n; i++)
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

                s++;
            }

            return result;
        }

        private int GetPartResult(int days, int k)
        {
            if (days < k)
                return 0;

            return days - k + 1;
        }
    }
}
