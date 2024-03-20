using NeuraSharp.Interfaces;
using System;
using System.Numerics;

namespace NeuraSharp.BuiltIn.BackwardAlgorithm
{
    public class DefaultBackwardAlgorithm<T> : IBackwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly ILossFunction<T> lossFunction;
        public DefaultBackwardAlgorithm(ILossFunction<T> lossFunction)
        {
            this.lossFunction = lossFunction;
        }
        public void Backward(INeuralLayer<T> iLayer, INeuralLayer<T> iPlusOneLayer, T[] target)
        {
            Parallel.For(0, iLayer.Gradients.Length, i =>
            {
                //Gradients[n] = (Weights[n]^T*Gradients[n+1]) * ActivationGradient
                T sum = T.Zero;
                for (int j = 0; j < iPlusOneLayer.Gradients.Length; j++)
                    sum += iPlusOneLayer.Gradients[j] * iPlusOneLayer.Weights[j][i];

                iLayer.Gradients[i] = sum * iLayer.Derivates[i];
            });
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
