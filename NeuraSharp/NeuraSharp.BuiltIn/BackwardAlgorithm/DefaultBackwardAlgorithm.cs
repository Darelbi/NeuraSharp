using NeuraSharp.Interfaces;
using System;
using System.Numerics;

namespace NeuraSharp.BuiltIn.BackwardAlgorithm
{
    public class DefaultBackwardAlgorithm<T> : IBackwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly ILossFunction<T> lossFunction;
        public DefaultBackwardAlgorithm(
            ILossFunction<T> lossFunction, 
            IParams<T> backParams)
        {
            this.lossFunction = lossFunction;
        }
        public void Backward(INeuralLayer<T> iLayer, INeuralLayer<T> iPlusOneLayer, T[] target)
        {
            /*iLayer.Gradients = iLayer.Weights * iPlusOneLayer.Gradients;

            Parallel.For(0, secondLayer.Outputs.Length, i =>
            {
                T sum = T.Zero;
                for (int j = 0; j < secondLayer.Weights[i].Length; j++)
                    sum += firstLayer.Outputs[j] * secondLayer.Weights[i][j] + secondLayer.Biases[i][j];

                secondLayer.Outputs[i] = activation.Compute(sum);
            });

            for (int i = 0; i < target.Length; i++)
            {
                // TODO initialize gradients in each batch
                firstRO.Gradients[i] += firstRO.Weights[i] * firstRO.PartialGradients[i];
            }*/
        }

        public void BackwardLast(INeuralLayer<T> Llayer, T[] target)
        {
            // https://www.sciencedirect.com/science/article/pii/S0893608021000800

            // delta J
            lossFunction.Derivate(target, Llayer.Outputs, Llayer.PartialGradients);

            for(int i=0; i<target.Length; i++)
            {
                // TODO initialize gradients in each batch
                Llayer.Gradients[i] += Llayer.Derivates[i]* Llayer.PartialGradients[i]; 
            }
        }
    }
}
