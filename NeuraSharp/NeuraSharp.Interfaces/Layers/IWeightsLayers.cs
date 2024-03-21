using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IWeightsLayers<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[][] Weights { get; set; }
    }
}
