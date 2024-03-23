using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers.Factory
{
    public class AdamOptimizerFactory<T> : IOptimizationAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IOptimizationAlgorithm<T> Create(IParams<T> optimizerParams)
        {
            return new AdamOptimizer<T>(optimizerParams);
        }

        public string GetName()
        {
            return "Adam";
        }
    }
}
