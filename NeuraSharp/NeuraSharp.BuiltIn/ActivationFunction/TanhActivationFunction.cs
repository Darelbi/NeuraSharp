/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ActivationFunction
{
    public class TanhActivationFunction<T> : IActivationFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T weightedSum)
        {
            return T.Tanh(weightedSum);
        }

        public T Derivate(T weightedSum)
        {
            var x = Compute(weightedSum);
            return (T.One - x * x);
        }
    }
}
