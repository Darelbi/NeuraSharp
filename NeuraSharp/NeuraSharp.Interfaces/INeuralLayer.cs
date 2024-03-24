using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuralLayer<T> 
        :   IBackwardInputLayer<T>, IBackwardOutputLayer<T>, IBackwardLastLayer<T>,
            ITotalGradientsLayer<T>, IBiasLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IActivationFunction<T> GetActivationFunction();

        public IRegularizationAlgorithm<T>[] GetRegularizers();
    }
}
