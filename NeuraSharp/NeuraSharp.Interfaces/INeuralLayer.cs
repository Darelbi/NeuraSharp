using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuralLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Outputs { get; set; }
        public T[] Derivates { get; set; }
        public T[][] Weights { get; set; }
        public T[][] Biases { get; set; }
        public bool[][] Disabled { get; set; } // allows in example to implement dropout
        public int[][] PreviousIndices { get; set; } // allows in example for sparse neural networks
    }
}
