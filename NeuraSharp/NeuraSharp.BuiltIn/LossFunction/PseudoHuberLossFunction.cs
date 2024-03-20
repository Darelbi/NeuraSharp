using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using System.Numerics;

namespace NeuraSharp.BuiltIn.LossFunction
{
    public class PseudoHuberLossFunction<T> : ILossFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private T delta;

        public PseudoHuberLossFunction( IParams<T> huberParams)
        {
            delta = huberParams.GetParameter(Params.Delta);
        }

        public T Compute(T[] output, T[] predictions)
        {
            T total = T.Zero;

            for (int i = 0; i < output.Length; i++)
            {
                T x = output[i];
                T y = predictions[i];

                var square = (x - y) / delta;
                var sqrt = T.Sqrt(T.One + square * square);
                total += delta * delta * (sqrt - T.One);
            }

            return total / T.CreateChecked(output.Length);
        }

        public void Derivate(T[] output, T[] predictions, T[] targetStore)
        {
            for (int i = 0; i < output.Length; i++)
            {
                T x = output[i];
                T y = predictions[i];

                var square = (x - y) / delta;
                var sqrt = T.Sqrt(T.One + square * square);
                targetStore[i] = (x - y) / sqrt;
            }
        }
    }
}
