/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// Interface for calling main functions on neural network, once its built, you need
    /// just this baby to do your stuff
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INeuraNetwork<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        /// <summary>
        /// After you are done with training you must compile the neural network to "lock" it
        /// this is because some algorithms (like DropOutRegularization) requires a final step
        /// to make the network meaningful.
        /// </summary>
        public void Compile();

        /// <summary>
        /// Classic method, YOU CALL it <paramref name="maxEpochs"/> times, providing your whole
        /// dataset composed by <paramref name="samples"/> at the given <paramref name="learningRate"/>.
        /// Note that the <paramref name="maxEpochs"/> is not a configuration. Is a promise you make
        /// to the Neural network that is used to know how many times <see cref="Fit(IEnumerable{List{ValueTuple{T[], T[]}}}, T, int)"/>
        ///  (this method) will be called.
        /// </summary>
        /// <param name="samples"> Enumerable of lists of samples. The lists represents the mini-batches of samples.
        /// batches are the number of times gradients are updated before backpropagating the weights. Typical batch
        /// sizes are 16 and 32.</param>
        /// <param name="learningRate"> Speed of learning a typical value is 0.01, but try what works bests</param>
        /// <param name="maxEpochs"> YOU PROMISE you will call <see cref="Fit(IEnumerable{List{ValueTuple{T[], T[]}}}, T, int)"/>
        /// this number of times. </param>
        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> samples, T learningRate, int maxEpochs, Action<T> errorCallback = null);

        /// <summary>
        /// You provide through the <see cref="IRunningMetadata{T}"/> interface all the metadata 
        /// required by various algorithms. this allows you to customize streaminng. In example
        /// if you have a really big training data set, you could in example stream batches of 32
        /// elements AND arbitrarily decide that each 3200 eelements (100 batches) makes an epoch
        /// and you may decide to stop after 5000 epochs even if the dataset is not finished.
        /// All of this by calling <see cref="Fit(IEnumerable{List{ValueTuple{T[], T[]}}}, IRunningMetadata{T})"/>
        /// just once potentially. You basically can customize things like epochs, steps, and even learning rate
        /// at runtime
        /// </summary>
        /// <param name="enumOfBatches"></param>
        /// <param name="source"></param>
        void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, IRunningMetadata<T> source, Action<T> errorCallback = null);

        /// <summary>
        /// If you have a neural network with 15 inputs values and 3 output values this function takes
        /// an array of 15 values and returns an array of 3 values.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        T[] Predict(T[] input);

        /// <summary>
        /// If you have a neural network with 15 inputs values and 3 output values this function takes
        /// an array of 15 values write those values in the <paramref name="output"/> array.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        void PredictInline(T[] input, T[] output);
    }
}
