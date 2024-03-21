using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface ILearningSource<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        T GetLearningRate();
    }
}
