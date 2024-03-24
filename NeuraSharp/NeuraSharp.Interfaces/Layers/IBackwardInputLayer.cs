using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IBackwardInputLayer<T>
        : IGradientsLayer<T>, IWeightsLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        
    }
}
