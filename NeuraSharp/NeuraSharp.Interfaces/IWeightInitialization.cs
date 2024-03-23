using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IWeightInitialization<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Initialize(IWeightsLayers<T> weights);
    }
}
