using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Sources
{
    public class DefaultRunningMetadata<T> : IRunningMetadata<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T learningRate;
        private readonly int maxEpochs;
        private int steps = 1;
        private int epochs = 1;

        public DefaultRunningMetadata(T learningRate, int maxEpochs)
        {
            this.learningRate = learningRate;
            this.maxEpochs = maxEpochs;
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
            return maxEpochs;
        }

        public void IncreaseEpoch()
        {
            epochs++;
        }
    }
}
