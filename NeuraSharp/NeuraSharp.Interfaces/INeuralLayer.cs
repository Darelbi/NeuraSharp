using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuralLayer<T> 
        :   IBackwardInputLayer<T>, IBackwardOutputLayer<T>, IBackwardLastLayer<T>,
            ITotalGradientsLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Biases { get; set; }

        public IActivationFunction<T> GetActivationFunction();

        public IRegularizationAlgorithm<T>[] GetRegularizers();

        public void Initialize(int index, int inputs, int outputs);
    }
}
