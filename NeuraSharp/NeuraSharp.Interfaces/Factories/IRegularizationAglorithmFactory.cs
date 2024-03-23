using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IRegularizationAglorithmFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IRegularizationAlgorithm<T> Create(IParams<T> optimizerParams);
    }
}
