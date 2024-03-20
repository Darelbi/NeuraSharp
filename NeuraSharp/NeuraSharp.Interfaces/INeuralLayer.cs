using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuralLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Outputs { get; set; }
        public T[] Derivates { get; set; }
        public T[] PartialGradients { get; set; }
        public T[] Gradients { get; set; }
        public T[][] Weights { get; set; }
        public T[] Biases { get; set; }

        public IActivationFunction<T> GetActivationFunction();

        public void Initialize(int inputs, int outputs);
    }
}
