﻿using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers.Factory
{
    public class GradientDescentOptimizerFactory<T> : IOptimizationAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IOptimizationAlgorithm<T> Create(IParams<T> optimizerParams)
        {
            return new GradientDescentOptimizer<T>();
        }

        public string GetName()
        {
            return "GradientDescent";
        }
    }
}
