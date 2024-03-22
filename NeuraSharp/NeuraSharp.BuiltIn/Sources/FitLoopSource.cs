using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Sources
{
    TODO RIPENSARE I PARAMETRI COME STEP ED EPOCH E RIPULIRE UN PO IL CODICE
    public class FitLoopSource<T> : INetworkTuningSource<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches;
        private readonly T learningRate;
        private int steps = 1;
        private int epochs = 1;

        public FitLoopSource(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, T learningRate)
        {
            this.enumOfBatches = enumOfBatches;
            this.learningRate = learningRate;
        }

        public IEnumerable<List<(T[] inputs, T[] outputs)>> GetEnumOfBatches()
        {
            foreach(var batch in enumOfBatches)
            {
                yield return batch;
            }
        }

        public int GetStep()
        {
            return steps;
        }

        public T GetLearningRate()
        {
            return learningRate;
        }

        public void IncreaseSteps()
        {
            ++steps;
        }

        public int GetEpoch()
        {
            return epochs;
        }

        public int GetTotalEpochs()
        {
            throw new NotImplementedException();
        }

        public void IncreaseEpoch()
        {
            throw new NotImplementedException();
        }
    }
}
