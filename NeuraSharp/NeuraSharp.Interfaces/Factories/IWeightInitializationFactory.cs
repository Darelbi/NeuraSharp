using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IWeightInitializationFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IWeightInitialization<T> Create(IParams<T> wInitParams);
    }
}
