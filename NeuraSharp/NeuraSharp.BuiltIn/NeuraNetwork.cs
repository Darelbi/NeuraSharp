using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn
{
    public class NeuraNetwork<T> : INeuraNetwork<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly INeuralLayer<T>[] layers;
        private readonly IForwardAlgorithm<T> forwardAlgorithm;
        private readonly IBackwardAlgorithm<T> backwardAlgorithm;

        public NeuraNetwork(
            INeuralLayer<T>[] layers,
            IForwardAlgorithm<T> forwardAlgorithm,
            IBackwardAlgorithm<T> backwardAlgorithm
            )
        {
            this.layers = layers;
            this.forwardAlgorithm = forwardAlgorithm;
            this.backwardAlgorithm = backwardAlgorithm;
        }

        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches)
        {
            foreach (var batch in enumOfBatches)
            {
                // this is a batch. 
                foreach (var sample in batch)
                {
                    // copy input in output of first layer
                    for (int i = 0; i < layers[0].Outputs.Length; i++)
                        layers[0].Outputs[i] = sample.inputs[i];

                    // forward propagation
                    for (int i = 0; i < layers.Length - 1; i++)
                        forwardAlgorithm.ForwardPrepare(layers[i], layers[i + 1]);

                    backwardAlgorithm.BackwardLast(layers.Last(), sample.outputs);
                    // optimizer step

                    for (int i = layers.Length - 2; i >= 0; i--)
                    {
                        backwardAlgorithm.Backward(layers[i], layers[i + 1], layers[i + 1].Gradients);
                        // optimizer step
                    }
                }

                // optimizer : optimize
            }
        }
    }
}
