using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IBiasLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Biases { get; set; }
    }
}
