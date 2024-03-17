using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ActivationFunction
{
    public class LeakyReLUActivationFunction<T> : IActivationFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T weightedSum)
        {
            if (weightedSum < T.Zero)
                return weightedSum * T.CreateChecked(0.01);

            return weightedSum;
        }

        public T Derivative(T weightedSum)
        {
            if (weightedSum < T.Zero)
                return T.CreateChecked(0.01);

            return T.One;
        }
    }
}
