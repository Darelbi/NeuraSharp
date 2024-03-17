using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.NeuronSummation
{
    public class StableNeuronSummation<T> : INeuronSummation<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetSum(T[] inputs)
        {
            //sum number from smallest to biggest ( absolute value) => reduce error!
            var sorted = inputs.OrderBy(x => T.Abs(x));
            T sum = T.Zero;
            foreach(var x in sorted)
            {
                sum += x;
            }
            return sum;
        }
    }
}
