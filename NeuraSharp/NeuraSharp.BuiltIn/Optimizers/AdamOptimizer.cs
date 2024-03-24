using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    /// <summary>
    /// https://github.com/aromanro/MachineLearning/blob/150635982d6067fbb3655fa1308c3bdff79ed384/MachineLearning/MachineLearning/GradientSolvers.h#L677
    /// </summary>
    public class AdamOptimizer<T> : IOptimizationAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly T B1;
        private readonly T B2;
        private readonly T Epsilon;

        public AdamOptimizer(IParams<T> adamParams)
        {
            B1 = adamParams.GetParameter(Params.Beta1);
            B2 = adamParams.GetParameter(Params.Beta2);
            Epsilon = adamParams.GetParameter(Params.Epsilon);
        }

        public T GetUpdatedLearningRate(T learningRate)
        {
            return learningRate;
        }

        public void Initialize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata)
        {
            int size = layer.Gradients.Length;

            variables.AddArrayVariable(Params.Momentum, new T[size]);
            variables.AddArrayVariable(Params.Velocity, new T[size]);
            variables.AddArrayVariable(Params.DeBiasedMomentum, new T[size]);
            variables.AddArrayVariable(Params.DeBiasedVelocity, new T[size]);
        }

        public void Optimize(IGradientsLayer<T> layer, ILayerAllocatedVariables<T> variables, IRunningMetadata<T> runningMetadata)
        {
            int step = runningMetadata.GetStep();

            var m = variables.GetArrayVariable(Params.Momentum);
            var v = variables.GetArrayVariable(Params.Velocity);
            var mt = variables.GetArrayVariable(Params.DeBiasedMomentum);
            var vt = variables.GetArrayVariable(Params.DeBiasedVelocity);

            Parallel.For(0, layer.Gradients.Length, i =>
            {
                var grad = layer.Gradients[i];
                m[i] = B1 * m[i] + (T.One - B1) * grad;
                v[i] = B2 * v[i] + (T.One - B2) * grad * grad;

                mt[i] = m[i] / (T.One - T.Pow(B1, T.CreateChecked(step)));
                vt[i] = v[i] / (T.One - T.Pow(B2, T.CreateChecked(step)));

                T partial = T.Sqrt(vt[i] + Epsilon);

                layer.Gradients[i] = mt[i] / partial;   // epsilon is reported outside sqrt in some papers
                                                        // and inside in others. I guess that makes little
                                                        // difference since vt is always positive anyway
            });
        }

        /*
          public void Backpropagation(double[] error)
    {
        double[] gradients = new double[weights.Length];
        double[] m = new double[weights.Length];
        double[] v = new double[weights.Length];

        for (int i = 0; i < weights.Length; i++)
        {
            // Calcola il gradiente
            gradients[i] = error[i] * ActivationDerivative(outputs[i]);

            // Aggiorna le stime del primo e del secondo momento
            m[i] = beta1 * m[i] + (1 - beta1) * gradients[i];
            v[i] = beta2 * v[i] + (1 - beta2) * Math.Pow(gradients[i], 2);

            // Correzione del bias
            double mHat = m[i] / (1 - Math.Pow(beta1, timestep));
            double vHat = v[i] / (1 - Math.Pow(beta2, timestep));

            // Aggiorna i pesi
            weights[i] -= learningRate * mHat / (Math.Sqrt(vHat) + epsilon);
        }
    }
         
         */

        /// <summary>
        /// https://cs231n.github.io/neural-networks-3/
        /// </summary>
        /// <param name="firstLayerReverseOrder"></param>
        /// <param name="secondLayerReverseOrder"></param>
        //public void Optimize(INeuralLayer<T> firstLayerReverseOrder, INeuralLayer<T> secondLayerReverseOrder)
        //{

        //    /*var Loss = lossFunction.Derivate( new T[] { }, new T[] { });    

        //    // Introduce Dynamic Parameters for Step
        //    T div1 = T.One / (T.One - T.Pow(B1, step));
        //    T div2 = T.One / (T.One. - T.Pow(B2, step));

        //    mb = beta1 * mb - (1. - beta1) * lossLinkGrad.sum();
        //    mb *= div1;
        //    sb = beta2 * sb + (1. - beta2) * lossLinkGrad.cwiseProduct(lossLinkGrad).sum();
        //    sb *= div2;

        //    b += BaseType::alpha * mb / sqrt(sb + eps);

        //    const double wAdj = (lossLinkGrad * BaseType::getInput().transpose())(0);

        //    mW = beta1 * mW - (1. - beta1) * wAdj;
        //    mW *= div1;
        //    sW = beta2 * sW + (1. - beta2) * wAdj * wAdj;
        //    sW *= div2;

        //    w += BaseType::alpha * mW / sqrt(sW + eps);

        //    return lossLinkGrad;*/
        //}


    }
}
