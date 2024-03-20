﻿using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IBackwardAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IForwardAlgorithm<T> Create();
    }
}