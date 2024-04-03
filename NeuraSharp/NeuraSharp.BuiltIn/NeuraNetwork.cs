/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.BuiltIn.Sources;
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn
{
    public class NeuraNetwork<T>(
        INeuralLayer<T>[] layers,
        IForwardAlgorithm<T> forwardAlgorithm,
        IBackwardAlgorithm<T> backwardAlgorithm,
        IOptimizationAlgorithm<T> optimizationAlgorithm,
        ILayerAllocatedConfiguration<T> layerAllocConfiguration
            ) : INeuraNetwork<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly INeuralLayer<T>[] layers = layers;
        private readonly IForwardAlgorithm<T> forwardAlgorithm = forwardAlgorithm;
        private readonly IBackwardAlgorithm<T> backwardAlgorithm = backwardAlgorithm;
        private readonly IOptimizationAlgorithm<T> optimizationAlgorithm = optimizationAlgorithm;
        private readonly ILayerAllocatedConfiguration<T> layerAllocConfiguration = layerAllocConfiguration;
        private bool trained = false;

        public void Compile()
        {
            Parallel.For(0, layers.Length, l =>
            {
                var regs = layers[l].GetRegularizers();
                foreach (var reg in regs)
                    reg.FinalNormalizationStep(layers[l]);
            });

            trained = true;
        }

        /// <summary>
        /// Faster function for predicting that does not allocate an array for that
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void PredictInline(T[] input, T[] output)
        {
            //if (!trained)
            //    throw new InvalidOperationException("Before predicting call 'Compile' to perform the finalization required steps for training ");

            Forward(input, output);
        }

        /// <summary>
        /// Easier to call function for predicting output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public T[] Predict(T[] input)
        {
            T[] output = new T[layers[^1].Outputs.Length];
            PredictInline(input, output);
            return output;
        }

        /// <summary>
        /// Allows streaming of resources and to set a custom epoch source, especially
        /// usefull for certain scenario.
        /// </summary>
        /// <param name="enumOfBatches"></param>
        /// <param name="source"></param>
        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, 
                        IRunningMetadata<T> source,
                        Action<T> errorCallback)
        {
            if (trained)
                throw new InvalidOperationException("Cannot Fit an already trained network. Some algorithms requires a final step after which it makes no sense further training");

            foreach (var batch in enumOfBatches)
            {
                // this is a batch. 

                ResetTotalGradients();

                foreach (var sample in batch)
                {
                    ForwardFit(sample, errorCallback);

                    Regularize();

                    Backward(sample, source);

                    AccumulateTotalGradients();

                    source.IncreaseSteps();
                }

                Propagation(source, batch.Count);
            }
        }

        private DefaultRunningMetadata<T> runningMetadata = null!;

        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, 
                        T learningRate, 
                        int maxEpochs,
                        Action<T> errorCallback)
        {
            runningMetadata ??= new DefaultRunningMetadata<T>(learningRate, maxEpochs);

            Fit(enumOfBatches, runningMetadata, errorCallback);
            runningMetadata.IncreaseEpoch();
        }

        private void Regularize()
        {
            Parallel.For(0, layers.Length, l =>
            {
                var regs = layers[l].GetRegularizers();
                foreach (var reg in regs)
                    reg.Regularize(layers[l]);
            });
        }

        /// <summary>
        /// Execute the final (back)propagation step, updating biases and weights
        /// </summary>
        /// <param name="batchSize"></param>
        private void Propagation(IRunningMetadata<T> tuning, int batchSize)
        {
            T scaleFactor =
                optimizationAlgorithm.GetUpdatedLearningRate(tuning.GetLearningRate())
                / T.CreateChecked(batchSize);
           
            for(int l=layers.Length-1; l>0;  l--) // not the best way to parallelize, but easy to read.
            {
                for (int i = 0; i < layers[l].Outputs.Length; i++)
                {
                    layers[l].Biases[i] -= scaleFactor * layers[l].TotalGradients[i];

                    for (int z = 0; z < layers[l].Weights[i].Length; z++)
                    {
                        // TODO: Accumulate weight changes, then transfer back at check points
                        // this requires 3 times more memory though...
                        layers[l].Weights[i][z] -= scaleFactor
                        //* layers[l].Weights[i][z] 
                        * layers[l - 1].Outputs[z]
                        * layers[l].TotalGradients[i];
                    }
                }
            }
        }

        private void AccumulateTotalGradients()
        {
            for (int l = 0; l < layers.Length; l++) // TODO parallelize only if dimension is above a certain treshold
                for (int i = 0; i < layers[l].Outputs.Length; i++)
                    layers[l].TotalGradients[i] += layers[l].Gradients[i];
        }

        private void ResetTotalGradients()
        {
            for (int l = 0; l < layers.Length; l++) // TODO parallelize only if dimension is above a certain treshold
                for (int i = 0; i < layers[l].Outputs.Length; i++)
                    layers[l].TotalGradients[i] = T.Zero;
        }

        /// <summary>
        /// Executes the forward step, preparing data for the backpropagation
        /// </summary>
        /// <param name="sample"></param>
        private void ForwardFit((T[] inputs, T[] outputs) sample, Action<T> errorCallback)
        {
            // copy input in output of first layer
            for (int i = 0; i < layers[0].Outputs.Length; i++)
                layers[0].Outputs[i] = sample.inputs[i];

            // forward propagation
            for (int i = 0; i < layers.Length - 1; i++)
                forwardAlgorithm.ForwardPrepare(layers[i], layers[i + 1]);

            errorCallback?.Invoke(backwardAlgorithm.GetLossFunction().Compute(sample.outputs, layers[^1].Outputs));
        }

        /// <summary>
        /// This function do the forward step and copies the output (propedeutic to call "predict")
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        private void Forward(T[] input, T[] output)
        {
            // copy input in output of first layer
            for (int i = 0; i < layers[0].Outputs.Length; i++)
                layers[0].Outputs[i] = input[i];

            // forward propagation
            for (int i = 0; i < layers.Length - 1; i++)
                forwardAlgorithm.Forward(layers[i], layers[i + 1]);

            // copy output 
            for (int i = 0; i < layers[^1].Outputs.Length; i++)
                output[i] = layers[^1].Outputs[i];
        }

        /// <summary>
        /// Execute the steps of the back(propagation). and calls the optimizer
        /// </summary>
        /// <param name="sample"></param>
        private void Backward((T[] inputs, T[] outputs) sample, IRunningMetadata<T> source)
        {
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                if (i == layers.Length - 1)
                    backwardAlgorithm.BackwardLast(layers.Last(), sample.outputs);
                else
                    backwardAlgorithm.Backward(layers[i], layers[i + 1], layers[i + 1].Gradients);

                layerAllocConfiguration.SetLayer(i); // allows the optimizer to retrieve
                                                     // the data for the corrrect layer
                optimizationAlgorithm.Optimize(layers[i], layerAllocConfiguration, source);
            }
        }
    }
}
