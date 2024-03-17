using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IForwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Forward(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer);

        public void ForwardPrepare(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer);
    }
}
