using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IBackwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Backward(INeuralLayer<T> iLayer, INeuralLayer<T> iPlusOneLayer, T[] target);

        public void BackwardLast(INeuralLayer<T> Llayer, T[] target);
    }
}
