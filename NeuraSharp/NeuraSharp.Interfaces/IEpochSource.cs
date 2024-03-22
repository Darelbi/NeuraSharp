using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuraSharp.Interfaces
{
    public interface IEpochSource
    {
        int GetEpoch();
        int GetTotalEpochs();
        void IncreaseEpoch();
    }
}
