/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ForwardAlgorithm.Factory
{
    /// <summary>
    /// https://github.com/aromanro/MachineLearning/blob/150635982d6067fbb3655fa1308c3bdff79ed384/MachineLearning/MachineLearning/GradientSolvers.h#L677
    /// https://blog.demofox.org/2024/02/11/gradient-descent-with-adam-in-plain-c/
    /// https://medium.com/the-ml-practitioner/how-to-implement-an-adam-optimizer-from-scratch-76e7b217f1cc
    /// https://github.com/dusanerdeljan/neural-network/blob/master/NeuralNetwork/src/optimizers/Adam.cpp
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultForwardAlgorithmFactory<T> : IForwardAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IForwardAlgorithm<T> Create()
        {
            return new DefaultForwardAlgorithm<T>();
        }

        public string GetName()
        {
            return "Default";
        }
    }
}
