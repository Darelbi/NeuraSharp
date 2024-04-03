/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization.Factory
{
    public class GlorotUniformInitializationFactory<T> : IWeightInitializationFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IWeightInitialization<T> Create(IParams<T> wInitParams)
        {
            return new GlorotUniformInitialization<T>();
        }

        public string GetName()
        {
            return "GlorotUniform";
        }
    }
}
