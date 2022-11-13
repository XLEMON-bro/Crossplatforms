using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CP_LAB_5_LIB
{
    public interface ILabWorker
    {
        public Task<string> GetOutputForLab(string Input);
    }
}
