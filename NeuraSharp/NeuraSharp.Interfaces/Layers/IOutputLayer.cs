using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IOutputLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] Outputs { get; set; }
    }
}
