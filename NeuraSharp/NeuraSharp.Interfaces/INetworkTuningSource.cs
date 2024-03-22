using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INetworkTuningSource<T>: IStepSource, IEpochSource,
            ILearningSource<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
    }
}
