using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuronSummation<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetSum(T[] inputs);
    }
}
