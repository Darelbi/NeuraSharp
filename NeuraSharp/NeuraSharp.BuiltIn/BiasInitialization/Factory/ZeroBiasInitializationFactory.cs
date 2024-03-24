using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.BiasInitialization.Factory
{
    public class ZeroBiasInitializationFactory<T> : IBiasInitializationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IBiasInitialization<T> Create(IParams<T> wInitParams)
        {
            return new ZeroBiasInitializer<T>();
        }

        public string GetName()
        {
            return "Zero";
        }
    }
}
