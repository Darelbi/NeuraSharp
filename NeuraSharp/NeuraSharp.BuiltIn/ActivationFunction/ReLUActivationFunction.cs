using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ActivationFunction
{
    public class ReLUActivationFunction<T> : IActivationFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T weightedSum)
        {
            return T.Max( T.Zero, weightedSum);
        }

        public T Derivate(T weightedSum)
        {
            if (weightedSum < T.Zero)
                return T.Zero;

            return T.One;
        }
    }
}
