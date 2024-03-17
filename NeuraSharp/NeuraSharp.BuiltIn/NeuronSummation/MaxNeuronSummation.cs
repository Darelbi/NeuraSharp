using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.NeuronSummation
{
    public class MaxNeuronSummation<T> : INeuronSummation<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetSum(T[] inputs)
        {
            return inputs.Max()!;
        }
    }
}
