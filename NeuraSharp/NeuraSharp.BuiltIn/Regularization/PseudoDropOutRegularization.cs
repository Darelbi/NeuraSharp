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
    /// Similar to dropout, but instead guarantee to drop only a fixed fraction of neurons
    /// This avoid dead layers on smaller networks, for bigger networks use DropOut
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PseudoDropOutRegularization<T>(IParams<T> param) : IRegularizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T chance = param.GetParameter(Params.DropoutChance);

        public void FinalNormalizationStep(INeuralLayer<T> layer)
        {
            int toBeZeroed = int.CreateChecked(chance * T.CreateChecked(layer.Outputs.Length));
            T realChance = T.CreateChecked(toBeZeroed) / T.CreateChecked(layer.Outputs.Length);

            for (int x = 0; x < layer.Weights.Length; x++)
            {
                for (int y = 0; y < layer.Weights[x].Length; y++)
                {
                    layer.Weights[x][y] = layer.Weights[x][y] * (T.One - realChance);
                }
            }
        }

        public void Regularize(INeuralLayer<T> layer)
        {
            int countZeros = 0;

            for(int i = 0; i<layer.Outputs.Length; i++)
            {
                if (layer.Outputs[i] == T.Zero)
                    countZeros++;
            }

            int toBeZeroed = int.CreateChecked(chance * T.CreateChecked(layer.Outputs.Length)) - countZeros;

            if (toBeZeroed >= layer.Outputs.Length)
                toBeZeroed = layer.Outputs.Length - 1; // guarantee at least one node.

            int count = toBeZeroed;

            // randomly select neurons to zero out. Note that this is exact algorithm
            // so if almost all neurons are discarded it may required quite some time
            // reaching further neurons. When I have time I'll use a shuffle list of indices.
            do
            {
                int index = Random.Shared.Next(0, layer.Outputs.Length);
                if (layer.Outputs[index] != T.Zero)
                {
                    layer.Outputs[index] = T.Zero;
                    toBeZeroed--;
                }
            }
            while (toBeZeroed > 0);

            // normalize remaning values to keep magnitude of output vector.
            T scalingFactor = T.CreateChecked(layer.Outputs.Length / (double) (layer.Outputs.Length - count));

            for (int i = 0; i < layer.Outputs.Length; i++)
            {
                layer.Outputs[i] = layer.Outputs[i] * scalingFactor;
            }
        }
    }
}
