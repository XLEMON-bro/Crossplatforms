using System;
using System.Threading.Tasks;

namespace CP_LAB_5_LIB.LABs
{
    public class LAB1 : ILabWorker
    {
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

            if (strMas.Length == 0)
            {
                return "Пусто!";
            }

            if (int.TryParse(strMas[0], out int value) && value >= 1)
            {
                if (value > 10000)
                {
                    return "Значение должно быть меньше 10000!";
                }

                var res = GetResult(value);

                if (res == 0)
                {
                    return "Ошибка попробуйте снова!";
                }

                return res.ToString();
            }
            else
            {
                return "Введите число!";
            }

            return null;
        }

        private int GetResult(int n)
        {
            if (n == 0)
                return 0;
            return GetSn(n);
        }

        private int GetSn(int n)
        {
            if (n <= 0)
            {
                return 0;
            }
            else
            {
                return GetSn(n - 1) + n * (n + 1) + GetNPlus(n);
            }
        }

        private int GetNPlus(int number)
        {
            if (number <= 1)
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
