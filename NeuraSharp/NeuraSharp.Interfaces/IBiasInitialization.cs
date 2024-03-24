using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IBiasInitialization<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Initialize(IBiasLayer<T> weights);
    }
}
