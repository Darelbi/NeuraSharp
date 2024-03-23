using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IOptimizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Initialize(ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata);

        public void Optimize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata);

        public T GetUpdatedLearningRate(T learningRate);
    }
}
