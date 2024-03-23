using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Layers;
using System.Drawing;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    internal class GradientDescentOptimizer<T> : IOptimizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T GetUpdatedLearningRate(T learningRate)
        {
            return learningRate;
        }

        public void Initialize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata)
        {
            
        }

        public void Optimize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata)
        {
            // do nothing that's ok, it's implemented in back and forward pass
        }
    }
}
