using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IRegularizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Regularize(INeuralLayer<T> layer);
    }
}
