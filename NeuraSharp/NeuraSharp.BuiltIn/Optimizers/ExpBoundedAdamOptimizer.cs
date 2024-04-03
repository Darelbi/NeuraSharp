/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class ExpBoundedAdamOptimizer<T>(IParams<T> adamParams) : BoundedAdamOptimizer<T>(adamParams) where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public override T GetAscendingBound(T minima, T maxima, T progresss)
        {
            return minima * T.Pow(maxima / minima, progresss);
        }

        public override T GetDescendingBound(T minima, T maxima, T progresss)
        {
            return maxima * T.Pow(minima / maxima, progresss);
        }
    }
}
