using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization
{
    internal class XavierInitialization<T> : IWeightInitialization<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public XavierInitialization()
        {
        }

        public void Initialize(IWeightsLayers<T> weights)
        {
            throw new NotImplementedException();
        }
    }
}
