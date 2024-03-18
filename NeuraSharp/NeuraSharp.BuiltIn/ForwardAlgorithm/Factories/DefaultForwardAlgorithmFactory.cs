using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ForwardAlgorithm.Factories
{
    public class StocasticDescentForwardFactory<T> : IForwardAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IForwardAlgorithm<T> Create(INeuronSummation<T> summation)
        {
            return new StocasticDescentForwardAlgorithm<T>(summation);
        }
    }
}
