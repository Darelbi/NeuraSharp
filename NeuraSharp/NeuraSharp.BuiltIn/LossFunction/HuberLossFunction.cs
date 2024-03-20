using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using System.Numerics;

namespace NeuraSharp.BuiltIn.LossFunction
{
    public class HuberLossFunction<T> : ILossFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private T delta;
        private T two;

        public HuberLossFunction(IParams<T> huberParams)
        {
            delta = huberParams.GetParameter(Params.Delta);
            two = T.One + T.One;
        }

        public T Compute(T[] output, T[] predictions)
        {
            var k = delta / two;
            T total = T.Zero;

            for (int i = 0; i < output.Length; i++)
            {
                T x = output[i];
                T y = predictions[i];

                if (T.Abs(x - y) <= delta)
                {
                    total += (x - y) * (x - y) / two;
                }
                else
                {
                    total += delta * (T.Abs(x - y) - k);
                }
            }

            return total / T.CreateChecked(output.Length);
        }

        public void Derivate(T[] output, T[] predictions, T[] targetStore)
        {
            for (int i = 0; i < output.Length; i++)
            {
                T x = output[i];
                T y = predictions[i];

                if (T.Abs(x - y) <= delta)
                {
                    targetStore[i] = -(x - y);
                }
                else
                {
                    targetStore[i] = delta * T.CreateChecked(T.Sign(x - y));
                }

            }
        }
    }
}
