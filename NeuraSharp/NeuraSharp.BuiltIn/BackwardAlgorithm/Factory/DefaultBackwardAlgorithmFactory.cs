/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using NeuraSharp.BuiltIn.BackwardAlgorithm;
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ForwardAlgorithm.Factory
{
    public class DefaultBackwardAlgorithmFactory<T> : IBackwardAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IBackwardAlgorithm<T> Create(ILossFunction<T> lossFunction)
        {
            return new DefaultBackwardAlgorithm<T>(lossFunction);
        }

        public string GetName()
        {
            return "Default";
        }
    }
}
