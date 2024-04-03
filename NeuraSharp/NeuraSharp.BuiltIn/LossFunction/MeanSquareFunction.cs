/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.LossFunction
{
    public class MeanSquareFunction<T>() : ILossFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T[] output, T[] predictions)
        {
            T total = T.Zero;
            T two = T.One + T.One;

            for (int i = 0; i < output.Length; i++)
            {
                T x = output[i];
                T y = predictions[i];
                T sq = (x - y)*(x-y);

                total += sq / two;
            }

            return total / T.CreateChecked(output.Length);
        }

        public void Derivate(T[] prediction, T[] target, T[] targetStore)
        {
            for (int i = 0; i < prediction.Length; i++)
            {
                T x = prediction[i];
                T y = target[i];

                targetStore[i] = x-y;
            }
        }
    }
}
