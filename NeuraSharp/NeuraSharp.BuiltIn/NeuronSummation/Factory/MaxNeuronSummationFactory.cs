using NeuraSharp.Interfaces.Factories;
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.NeuronSummation.Factory
{
    public class MaxNeuronSummationFactory<T> : ISummationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public INeuronSummation<T> Create()
        {
            return new MaxNeuronSummation<T>();
        }

        public string GetName()
        {
            return "Max";
        }
    }
}
