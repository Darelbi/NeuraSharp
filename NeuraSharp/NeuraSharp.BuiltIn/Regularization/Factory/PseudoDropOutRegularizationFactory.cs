/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Regularization.Factory
{
    public class PseudoDropOutRegularizationFactory<T> : IRegularizationAglorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IRegularizationAlgorithm<T> Create(IParams<T> optimizerParams)
        {
            return new PseudoDropOutRegularization<T>(optimizerParams);
        }

        public string GetName()
        {
            return "PseudoDropOut";
        }
    }
}
