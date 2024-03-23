using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface ILossFunctionFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public ILossFunction<T> Create(IParams<T> lossParams);
    }
}
