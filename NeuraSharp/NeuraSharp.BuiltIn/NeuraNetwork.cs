using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn
{
    public class NeuraNetwork<T> : INeuraNetwork<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly INeuralLayer<T>[] layers;
        private readonly IForwardAlgorithm<T> forwardAlgorithm;

        public NeuraNetwork(INeuralLayer<T>[] layers, IForwardAlgorithm<T> forwardAlgorithm)
        {
            this.layers = layers;
            this.forwardAlgorithm = forwardAlgorithm;
        }

        public void Fit(IEnumerable<List<T[]>> enumOfBatches)
        {
            foreach(var batch in enumOfBatches)
            {
                // this is a batch
                foreach(var sample in batch)
                {
                    for(int i=0; i<layers.Length-1; i++)
                        forwardAlgorithm.ForwardPrepare(layers[i], layers[i+1]);
                }

                // TODO: Backpropagation
            }
        }
    }
}
