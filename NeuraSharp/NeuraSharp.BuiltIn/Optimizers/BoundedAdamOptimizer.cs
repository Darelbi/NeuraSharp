using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    /// <summary>
    /// Adam, but generalize better and outperform SGD also in cases where SGD performs better than Adam
    /// https://www.mdpi.com/2076-3417/9/17/3569
    /// Source: Tang, M.; Huang, Z.; Yuan, Y.; Wang, C.; Peng, Y. A Bounded Scheduling Method for
    /// Adaptive Gradient Methods. Appl. Sci. 2019, 9, 3569. https://doi.org/10.3390/app9173569
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoundedAdamOptimizer<T> : IOptimizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T B1;
        private readonly T B2;
        private readonly T Epsilon;
        private readonly INetworkTuningSource<T> source;

        public BoundedAdamOptimizer(IParams<T> adamParams, INetworkTuningSource<T> source)
        {
            B1 = adamParams.GetParameter(Params.Beta1);
            B2 = adamParams.GetParameter(Params.Beta2);
            Epsilon = adamParams.GetParameter(Params.Epsilon);
            this.source = source;
        }

        public T GetUpdatedLearningRate(T learningRate)
        {
            return T.One; // this algorithm takes control of learning rate
        }

        public void Initialize(ILayerAllocatedVariables<T> variables)
        {
            int size = variables.GetIntVariable(Params.LayerSize);

            variables.AddArrayVariable(Params.Momentum, new T[size]);
            variables.AddArrayVariable(Params.Velocity, new T[size]);
            variables.AddArrayVariable(Params.DeBiasedMomentum, new T[size]);
            variables.AddArrayVariable(Params.DeBiasedVelocity, new T[size]);
        }

        public void Optimize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables)
        {
            int size = variables.GetIntVariable(Params.LayerSize);
            int step = source.GetStep();

            var m = variables.GetArrayVariable(Params.Momentum);
            var v = variables.GetArrayVariable(Params.Velocity);
            var mt = variables.GetArrayVariable(Params.DeBiasedMomentum);
            var vt = variables.GetArrayVariable(Params.DeBiasedVelocity);

            T learningRate = source.GetLearningRate();
            T num = T.Sqrt(T.One - T.Pow(B2, T.CreateChecked(step)));
            T den = (T.One - T.Pow(B1, T.CreateChecked(step)));
            T maxima = T.CreateChecked(0.5);
            T minima = T.CreateChecked(0.001);

            Parallel.For(0, size, i =>
            {
                var grad = layer.Gradients[i];
                m[i] = B1 * m[i] + (T.One - B1) * grad;
                v[i] = B2 * v[i] + (T.One - B2) * grad * grad;

                mt[i] = m[i] / (T.One - T.Pow(B1, T.CreateChecked(step)));
                vt[i] = v[i] / (T.One - T.Pow(B2, T.CreateChecked(step)));

                T partial = T.Sqrt(vt[i] + Epsilon);
                T newLearning = (num / den) * learningRate / partial;

                layer.Gradients[i] = newLearning * mt[i] / partial;
            });
        }
    }
}
