﻿using System.Numerics;

namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// Activation functions works like a logic gate, they take the weighted sum of previous
    /// layers and map it to a value providing a sort of step that allows to select between
    /// a "active" or "not active". 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IActivationFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T weightedSum);

        public T Derivative(T weightedSum);
    }
}
