﻿using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ForwardAlgorithm
{
    public class DefaultForwardAlgorithm<T> : IForwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public DefaultForwardAlgorithm()
        {

        }

        private static void ForwardStep(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer, bool prepare)
        {
            var activation = secondLayer.GetActivationFunction();

            Parallel.For(0, secondLayer.Outputs.Length, i =>
            {
                T sum = T.Zero;
                for (int j = 0; j < secondLayer.Weights[i].Length; j++)
                    sum += firstLayer.Outputs[j] * secondLayer.Weights[i][j] + secondLayer.Biases[i];

                secondLayer.Outputs[i] = activation.Compute(sum);

                if (prepare)
                    secondLayer.Derivates[i] = activation.Derivate(sum);
            });
        }

        public void Forward(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer)
        {
            DefaultForwardAlgorithm<T>.ForwardStep(firstLayer, secondLayer, false);
        }

        public void ForwardPrepare(INeuralLayer<T> firstLayer, INeuralLayer<T> secondLayer)
        {
            DefaultForwardAlgorithm<T>.ForwardStep(firstLayer, secondLayer, true);
        }
    }
}
