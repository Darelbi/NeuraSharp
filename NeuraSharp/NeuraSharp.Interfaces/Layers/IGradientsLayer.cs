using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IGradientsLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Gradients { get; set; }
    }
}
