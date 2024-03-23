using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface ISummationFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public INeuronSummation<T> Create();
    }
}
