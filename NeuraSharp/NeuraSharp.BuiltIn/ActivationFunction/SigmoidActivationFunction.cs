using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ActivationFunction
{
    public class SigmoidActivationFunction<T> : IActivationFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T weightedSum)
        {
            return T.One / (T.One + T.Exp(-weightedSum));
        }

        public T Derivate(T weightedSum)
        {
            return Compute(weightedSum) * (T.One - Compute(weightedSum));
        }
    }
}
