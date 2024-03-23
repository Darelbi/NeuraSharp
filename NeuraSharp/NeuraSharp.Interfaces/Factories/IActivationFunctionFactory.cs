using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IActivationFunctionFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IActivationFunction<T> Create(IParams<T> activationParams);
    }
}
