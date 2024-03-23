using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface ILossFunctionFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public ILossFunction<T> Create(IParams<T> lossParams);
    }
}
