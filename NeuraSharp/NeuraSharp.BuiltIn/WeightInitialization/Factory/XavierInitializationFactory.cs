using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization.Factory
{
    public class XavierInitializationFactory<T> : IWeightInitializationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IWeightInitialization<T> Create(IParams<T> wInitParams)
        {
            return new XavierInitialization<T>();
        }

        public string GetName()
        {
            return "Xavier";
        }
    }
}
