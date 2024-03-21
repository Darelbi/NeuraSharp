using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IBackwardInputLayer<T>
        : IGradientsLayer<T>, IWeightsLayers<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        
    }
}
