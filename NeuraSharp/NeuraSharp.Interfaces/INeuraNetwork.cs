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
        /// Using a enumerable allows you to fine tune batch size and epochs.
        /// Number of elements in the list is the batch size. The fit method
        /// will run until the enumerable runs out of elements. 
        /// Enumerable also allows streaming big datasets without using too 
        /// much RAM memory.
        /// 
        /// There are no Epochs because you can implement that from your enumerable
        /// </summary>
        /// <param name="samples"></param>
        public void Fit(IEnumerable<List<(T[] inputs, T[] outputs)>> samples);
    }
}
