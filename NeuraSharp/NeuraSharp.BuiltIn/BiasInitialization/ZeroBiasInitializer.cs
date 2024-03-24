using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.BiasInitialization
{
    public class ZeroBiasInitializer<T> : IBiasInitialization<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public ZeroBiasInitializer()
        {
        }
        public void Initialize(IBiasLayer<T> weights)
        {
            // do nothing. C# already initialize arrays to 0.
        }
    }
}
