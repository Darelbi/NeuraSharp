using System.Numerics;

namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// Algorithm for computing the summation of neurons input (output of previous layer)
    /// each one scaled by its own weight. There are some algorithms used in research
    /// and almost no library allowing their customization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INeuronSummation<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetSum(T[] inputs);
    }
}
