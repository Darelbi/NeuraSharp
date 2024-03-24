using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization.Factory
{
    public class UniformInitializationFactory<T> : IWeightInitializationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IWeightInitialization<T> Create(IParams<T> wInitParams)
        {
            return new UniformInitialization<T>(wInitParams);
        }

        public string GetName()
        {
            return "Uniform";
        }
    }
}
