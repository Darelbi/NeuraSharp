using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Layers
{
    [Skip]
    public class BaseNeuralLayer<T> : INeuralLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Outputs { get; set; }
        public T[] Derivates { get; set; }
        public T[][] Weights { get; set; }
        public T[][] Biases { get; set; }
        public bool[][] Disabled { get; set; }
        public int[][] PreviousIndices { get; set; } // allows in example for sparse neural networks
    }
}
