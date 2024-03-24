using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.WeightInitialization
{
    /// <summary>
    /// This initialization works best for sigmoid or tanh functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HeUniformInitialization<T> : IWeightInitialization<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public HeUniformInitialization()
        {
        }

        public void Initialize(IWeightsLayer<T> weights)
        {
            for(int l = 0; weights.Weights.Length > l; l++)
            {
                // Glorot initialization uses 6 for uniform distribution (like Random.Shared.NextDouble)
                // the number 2 should be used for a NORMAL distribution
                T sq = T.Sqrt(T.CreateChecked( 6.0 /(weights.Weights[l].Length)) );
                T min = -sq;
                T max = sq;

                for(int j=0; weights.Weights[l].Length > j; j++)
                {
                    T rnd = T.CreateChecked(Random.Shared.NextDouble());
                    weights.Weights[l][j] = min + rnd * (max - min);
                }
            }
        }
    }
}
