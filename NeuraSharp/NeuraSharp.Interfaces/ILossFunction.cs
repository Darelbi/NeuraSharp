/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// https://machinelearningmastery.com/adam-optimization-from-scratch/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILossFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T[] output, T[] predictions);

        public void Derivate(T[] output, T[] predictions, T[] targetStore);
    }
}
