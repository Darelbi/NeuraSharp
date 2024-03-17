using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.NeuronSummation
{
    public class FastNeuronSummation<T> : INeuronSummation<T> where T:INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetSum(T[] inputs)
        {
            T sum = inputs[0];
            for(int i=1; i<inputs.Length;i++)
                sum += inputs[i];
            return sum;
        }
    }
}
