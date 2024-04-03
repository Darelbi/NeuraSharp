/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Regularization
{
    /// <summary>
    /// Realdrop out, for networks of 30+ neurons it should not matter even if 1 time we get a dead layer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DropOutRegularization<T>(IParams<T> param) : IRegularizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T chance = param.GetParameter(Params.DropoutChance);

        public void FinalNormalizationStep(INeuralLayer<T> layer)
        {
            for(int x = 0; x< layer.Weights.Length; x++)
            {
                for(int y=0; y< layer.Weights[x].Length; y++)
                {
                    layer.Weights[x][y] = layer.Weights[x][y] * (T.One - chance);
                }
            }
        }

        public void Regularize(INeuralLayer<T> layer)
        {
            int count = 0;

            for(int i = 0; i<layer.Outputs.Length; i++)
            {
                T foundChance = T.CreateChecked(Random.Shared.NextDouble());

                if(foundChance<chance)
                {
                    layer.Outputs[i] = T.Zero;
                    count++;
                }
            }

            // normalize remaning values to keep magnitude of output vector.
            T scalingFactor = T.CreateChecked(layer.Outputs.Length / (double) (layer.Outputs.Length - count));

            for (int i = 0; i < layer.Outputs.Length; i++)
            {
                layer.Outputs[i] = layer.Outputs[i] * scalingFactor;
            }
        }
    }
}
