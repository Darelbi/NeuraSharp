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
    public class PseudoHuberLossFunction<T>(IParams<T> huberParams) : ILossFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private T delta = huberParams.GetParameter(Params.Delta);

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

        public void Derivate(T[] prediction, T[] target, T[] targetStore)
        {
            for (int i = 0; i < prediction.Length; i++)
            {
                T x = prediction[i];
                T y = target[i];

                var square = (x - y) / delta;
                var sqrt = T.Sqrt(T.One + square * square);
                targetStore[i] = (x - y) / sqrt;
            }
        }
    }
}
