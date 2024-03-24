using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization.Factory
{
    public class HeUniformInitializationFactory<T> : IWeightInitializationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IWeightInitialization<T> Create(IParams<T> wInitParams)
        {
            return new HeUniformInitialization<T>();
        }

        public string GetName()
        {
            return "HeUniform";
        }
    }
}
