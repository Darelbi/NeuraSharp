using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IOptimizationAlgorithmFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IOptimizationAlgorithm<T> Create(IParams<T> optimizerParams);
    }
}
