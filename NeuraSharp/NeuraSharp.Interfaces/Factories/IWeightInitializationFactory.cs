/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IWeightInitializationFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IWeightInitialization<T> Create(IParams<T> wInitParams);
    }
}
