﻿using System.Numerics;

namespace NeuraSharp.Interfaces.Factories
{
    public interface IBackwardAlgorithmFactory<T> : IFactory where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IBackwardAlgorithm<T> Create(ILossFunction<T> lossFunction);
    }
}
