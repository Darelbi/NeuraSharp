/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class GradientDescentOptimizer<T> : IOptimizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetUpdatedLearningRate(T learningRate)
        {
            return learningRate;
        }

        public void Initialize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata)
        {
            
        }

        public void Optimize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata)
        {
            // do nothing that's ok, it's implemented in back and forward pass

            /* HERE's the code:
             * 
             * 
                 *  T sum = T.Zero;
                    for (int j = 0; j < iPlusOneLayer.Gradients.Length; j++)
                        sum += iPlusOneLayer.Gradients[j] * iPlusOneLayer.Weights[j][i];

                    iLayer.Gradients[i] = sum * iLayer.Derivates[i];

            AND

                    layers[l].Biases[i] -= scaleFactor * layers[l].TotalGradients[i];

                    for (int z = 0; z < layers[l].Weights[i].Length; z++)
                    {
                        // TODO: Accumulate weight changes, then transfer back at check points
                        // this requires 3 times more memory though...
                        layers[l].Weights[i][z] -= scaleFactor * layers[l].Weights[i][z] * layers[l].TotalGradients[i];
                    }
             
                 Original Project: https://github.com/Darelbi/NeuraSharp/ */
        }
    }
}
