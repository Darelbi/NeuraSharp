using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IActivationDerivatesLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
    }
}
