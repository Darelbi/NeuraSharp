using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IBackwardLastLayer<T> 
        : IGradientsLayer<T>, IDerivateLayer<T>, IOutputLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
    }
}
