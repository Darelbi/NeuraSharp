/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuralLayer<T> 
        :   IBackwardInputLayer<T>, IBackwardOutputLayer<T>, IBackwardLastLayer<T>,
            ITotalGradientsLayer<T>, IBiasLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IActivationFunction<T> GetActivationFunction();

        public IRegularizationAlgorithm<T>[] GetRegularizers();
    }
}
