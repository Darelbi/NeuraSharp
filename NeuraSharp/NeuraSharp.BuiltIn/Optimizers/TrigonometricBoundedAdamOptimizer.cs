/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class TrigonometricBoundedAdamOptimizer<T>(IParams<T> adamParams) : BoundedAdamOptimizer<T>(adamParams) where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T two = T.One + T.One;

        public override T GetAscendingBound(T minima, T maxima, T progresss)
        {
            return minima + (maxima - minima)*T.Sin(progresss * T.Pi / two);
        }

        public override T GetDescendingBound(T minima, T maxima, T progresss)
        {
            return maxima - (maxima - minima) * T.Sin(progresss * T.Pi / two);
        }
    }
}
