using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IDerivateLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Derivates { get; set; }
    }
}
