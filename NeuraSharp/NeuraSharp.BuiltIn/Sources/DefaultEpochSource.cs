using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Sources
{
    public class DefaultEpochSource<T> : INetworkTuningSource<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches;
        private readonly T learningRate;
        private int epoch = 1;

        public DefaultEpochSource(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, T learningRate)
        {
            this.enumOfBatches = enumOfBatches;
            this.learningRate = learningRate;
        }

        public IEnumerable<List<(T[] inputs, T[] outputs)>> GetEnumOfBatches()
        {
            foreach(var batch in enumOfBatches)
            {
                yield return batch;
                epoch++;
            }
        }

        public int GetStep()
        {
            return epoch;
        }

        public T GetLearningRate()
        {
            return learningRate;
        }
    }
}
