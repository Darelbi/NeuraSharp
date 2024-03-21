using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IBackwardOutputLayer<T> 
        : IGradientsLayer<T>, IDerivateLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
    }
}
