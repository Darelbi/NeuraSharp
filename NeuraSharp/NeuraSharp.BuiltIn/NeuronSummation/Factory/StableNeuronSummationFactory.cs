using NeuraSharp.Interfaces.Factories;
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.NeuronSummation.Factory
{
    public class StableNeuronSummationFactory<T> : ISummationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public INeuronSummation<T> Create()
        {
            return new StableNeuronSummation<T>();
        }

        public string GetName()
        {
            return "Stable";
        }
    }
}
