using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CP_LAB_5_LIB.LABs
{
    public class LAB3 : ILabWorker
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

            return null;
        }
    }
}
