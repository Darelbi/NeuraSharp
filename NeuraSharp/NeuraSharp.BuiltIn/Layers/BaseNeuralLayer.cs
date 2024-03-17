using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Layers
{
    [Skip]
    public class BaseNeuralLayer<T> : INeuralLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly IActivationFunction<T> activation;

        public T[] Outputs { get; set; }
        public T[] Derivates { get; set; }
        public T[] Errors { get; set; }
        public T[][] Weights { get; set; }
        public T[][] Biases { get; set; }
        public bool[][] Inactive { get; set; }
        public int[][] PreviousIndices { get; set; }

        public BaseNeuralLayer(IActivationFunction<T> activation)
        {
            this.activation = activation;
        }

        public IActivationFunction<T> GetActivationFunction()
        {
            // the function is layer specific that's why we need it there
            return activation;
        }
    }
}
