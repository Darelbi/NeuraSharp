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
        /// Using a enumerable allows you to fine tune batch size and epochs.
        /// Number of elements in the list is the batch size. The fit method
        /// will run until the enumerable runs out of elements. 
        /// Enumerable also allows streaming big datasets without using too 
        /// much RAM memory.
        /// 
        /// By default each Batch increase the epoch counter, but you
        /// can call the other overload method to provide you own epoc provider
        /// </summary>
        /// <param name="samples"></param>
        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> samples);

        /// <summary>
        /// Allows streaming the input/ouput pairs for training the network
        /// and to regulate size of mini-batches by the size of the List.
        /// By implementing <see cref="IEpochSource{T}"/> you are allowed
        /// to progress the epoch counter in a custom way during your streaming.
        /// Typically the class implementing the interface is the same
        /// returning the enumerable.
        /// </summary>
        /// <param name="enumOfBatches"></param>
        /// <param name="source"></param>
        void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, INetworkTuningSource<T> source);

        /// <summary>
        /// Easier to call function for predicting output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        T[] Predict(T[] input);

        /// <summary>
        /// Faster function for predicting that does not allocate an array for that
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        void PredictInline(T[] input, T[] output);
    }
}
