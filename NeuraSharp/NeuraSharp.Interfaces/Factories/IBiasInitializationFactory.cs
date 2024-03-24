using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IBiasInitializationFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IBiasInitialization<T> Create(IParams<T> wInitParams);
    }
}
