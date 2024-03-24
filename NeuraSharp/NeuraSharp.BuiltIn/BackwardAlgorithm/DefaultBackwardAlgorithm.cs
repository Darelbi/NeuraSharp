using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.BackwardAlgorithm
{
    public class DefaultBackwardAlgorithm<T>(ILossFunction<T> lossFunction) : IBackwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly ILossFunction<T> lossFunction = lossFunction;

        public void Backward(IBackwardOutputLayer<T> iLayer, IBackwardInputLayer<T> iPlusOneLayer, T[] target)
        {
            Parallel.For(0, iLayer.Gradients.Length, i =>
            {
                // In original paper
                // https://www.sciencedirect.com/science/article/pii/S0893608021000800
                // The weights matrix is on n layer, but here we have it on following layer
                // Gradients[n] = (Weights[n]^T*Gradients[n+1]) * ActivationGradient[n]
                // becomes
                // Gradients[n] = (Weights[n+1]^T*Gradients[n+1]) * ActivationGradient[n]
                T sum = T.Zero;
                for (int j = 0; j < iPlusOneLayer.Gradients.Length; j++)
                    sum += iPlusOneLayer.Gradients[j] * iPlusOneLayer.Weights[j][i];

                iLayer.Gradients[i] = sum * iLayer.Derivates[i];
            });
        }

        public void BackwardLast(IBackwardLastLayer<T> Llayer, T[] target)
        {
            // https://www.sciencedirect.com/science/article/pii/S0893608021000800

            // delta J. Loss function derivative is calculated at the end of the network
            lossFunction.Derivate(target, Llayer.Outputs, Llayer.Gradients);

            for(int i=0; i<target.Length; i++)
            {
                // TODO initialize gradients in each batch
                Llayer.Gradients[i] = Llayer.Derivates[i]* Llayer.Gradients[i]; 
            }
        }
    }
}
