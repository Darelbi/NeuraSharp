using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization
{
    /// <summary>
    /// This initialization works best for sigmoid or tanh functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UniformInitialization<T> : IWeightInitialization<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T min;
        private readonly T max;

        public UniformInitialization(IParams<T> uniformParams)
        {
            min = uniformParams.GetParameter(Params.Min);
            max = uniformParams.GetParameter(Params.Max);
        }

        public void Initialize(IWeightsLayers<T> weights)
        {
            for(int l = 0; weights.Weights.Length > l; l++)
            {
                for(int j=0; weights.Weights[l].Length > j; j++)
                {
                    T rnd = T.CreateChecked(Random.Shared.NextDouble());
                    weights.Weights[l][j] = min + rnd * (max - min);
                }
            }
        }
    }
}
