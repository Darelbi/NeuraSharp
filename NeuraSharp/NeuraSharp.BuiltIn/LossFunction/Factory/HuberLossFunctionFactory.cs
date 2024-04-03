/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.LossFunction.Factory
{
    public class HuberLossFunctionFactory<T> : ILossFunctionFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public ILossFunction<T> Create(IParams<T> huberParams)
        {
            return new HuberLossFunction<T>(huberParams);
        }

        public string GetName()
        {
            return "Huber";
        }
    }
}
