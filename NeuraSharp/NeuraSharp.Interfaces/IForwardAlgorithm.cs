/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IForwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Forward(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer);

        public void ForwardPrepare(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer);
    }
}
