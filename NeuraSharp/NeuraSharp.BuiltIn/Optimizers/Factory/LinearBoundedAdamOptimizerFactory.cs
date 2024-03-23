using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers.Factory
{
    public class LinearBoundedAdamOptimizerFactory<T> : IOptimizationAlgorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IOptimizationAlgorithm<T> Create(IParams<T> optimizerParams)
        {
            return new LinearBoundedAdamOptimizer<T>(optimizerParams);
        }

        public string GetName()
        {
            return "BoundedAdam_Linear";
        }
    }
}
