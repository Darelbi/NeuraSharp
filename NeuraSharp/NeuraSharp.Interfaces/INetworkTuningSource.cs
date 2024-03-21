using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INetworkTuningSource<T>: IStepSource,
            ILearningSource<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
    }
}
