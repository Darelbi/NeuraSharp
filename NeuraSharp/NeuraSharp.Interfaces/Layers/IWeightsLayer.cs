using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IWeightsLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[][] Weights { get; set; }
    }
}
