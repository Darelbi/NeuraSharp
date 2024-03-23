using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IForwardAlgorithmFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IForwardAlgorithm<T> Create();
    }
}
