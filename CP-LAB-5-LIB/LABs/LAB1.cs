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
            return null;
        }
    }
}
