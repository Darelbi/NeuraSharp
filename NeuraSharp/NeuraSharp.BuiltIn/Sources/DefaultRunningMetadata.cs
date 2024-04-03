/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Sources
{
    public class DefaultRunningMetadata<T>(T learningRate, int maxEpochs) : IRunningMetadata<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T learningRate = learningRate;
        private readonly int maxEpochs = maxEpochs;
        private int steps = 1;
        private int epochs = 1;

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
