/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using System.Numerics;

namespace NeuraSharp.BuiltIn.LossFunction
{
    public class HuberLossFunction<T>(IParams<T> huberParams) : ILossFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T delta = huberParams.GetParameter(Params.Delta);
        private readonly  T two = T.One + T.One;

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

        public void Derivate(T[] prediction, T[] target, T[] targetStore)
        {
            for (int i = 0; i < prediction.Length; i++)
            {
                T x = prediction[i];
                T y = target[i];

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
