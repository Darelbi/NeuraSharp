/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IRegularizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Regularize(INeuralLayer<T> layer);

        public void FinalNormalizationStep(INeuralLayer<T> layer);
    }
}
