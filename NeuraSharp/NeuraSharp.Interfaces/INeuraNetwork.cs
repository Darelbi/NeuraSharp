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
        /// Some algorithms, not all, requires to know in advance number of samples
        /// and number of epochs. An epoch is going through all the trainin data set
        /// If you use this method some assumptions are made:
        /// 1) IEnumerable is NOT INFINITE
        /// 2) IEnumerable is your whole data set
        /// 3) Each time you call Fit(samples) the epoch counter is increased by 1
        /// 4) Data is divided into batches which size is equal to List size.
        /// 5) Using IEnumerable allows you to stream the dataset
        /// 6) You must provide the number of epochs: required by some algorithm
        /// 7) You have to call Fit explicitly each time you want to make an epoch
        /// 8) IEnumerable IS NOT streamed multiple by times, just once.
        /// </summary>
        /// <param name="samples">Enum of batches, each batch is a list which contains some input/output pairs. The whole dataset</param>
        /// <param name="maxEpochs">You promise you call <see cref="Fit(IEnumerable{List{ValueTuple{T[], T[]}}}, int)"/> this number of times</param>
        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> samples, int maxEpochs);

        /// <summary>
        /// You provide through the <see cref="IRunningMetadata{T}"/> interface all the metadata 
        /// required by various algorithms. this allows you to customize streaminng. In example
        /// if you have a really big training data set, you could in example stream batches of 32
        /// elements AND arbitrarily decide that each 3200 eelements (100 batches) makes an epoch
        /// and you may decide to stop after 5000 epochs even if the dataset is not finished.
        /// All of this by calling <see cref="Fit(IEnumerable{List{ValueTuple{T[], T[]}}}, IRunningMetadata{T})"/>
        /// just once potentially.
        /// </summary>
        /// <param name="enumOfBatches"></param>
        /// <param name="source"></param>
        void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> enumOfBatches, IRunningMetadata<T> source);

        /// <summary>
        /// Easier to call function for predicting output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        T[] Predict(T[] input);

        /// <summary>
        /// Faster function for predicting that does not allocate an array for that, but you must provide 
        /// an array where result will be placed in.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        void PredictInline(T[] input, T[] output);
    }
}
