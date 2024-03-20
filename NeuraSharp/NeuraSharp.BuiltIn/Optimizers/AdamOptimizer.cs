using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    /// <summary>
    /// https://github.com/aromanro/MachineLearning/blob/150635982d6067fbb3655fa1308c3bdff79ed384/MachineLearning/MachineLearning/GradientSolvers.h#L677
    /// </summary>
    public class AdamOptimizer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly ILossFunction<T> lossFunction;
        private readonly T B1;
        private readonly T B2;

        public AdamOptimizer(ILossFunction<T> lossFunction, IParams<T> adamParams)
        {
            this.lossFunction = lossFunction;
            B1 = adamParams.GetParameter(Params.Beta1);
            B2 = adamParams.GetParameter(Params.Beta2);
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
        public void Optimize(INeuralLayer<T> firstLayerReverseOrder, INeuralLayer<T> secondLayerReverseOrder)
        {

            /*var Loss = lossFunction.Derivate( new T[] { }, new T[] { });    

            // Introduce Dynamic Parameters for Step
            T div1 = T.One / (T.One - T.Pow(B1, step));
            T div2 = T.One / (T.One. - T.Pow(B2, step));

            mb = beta1 * mb - (1. - beta1) * lossLinkGrad.sum();
            mb *= div1;
            sb = beta2 * sb + (1. - beta2) * lossLinkGrad.cwiseProduct(lossLinkGrad).sum();
            sb *= div2;

            b += BaseType::alpha * mb / sqrt(sb + eps);

            const double wAdj = (lossLinkGrad * BaseType::getInput().transpose())(0);

            mW = beta1 * mW - (1. - beta1) * wAdj;
            mW *= div1;
            sW = beta2 * sW + (1. - beta2) * wAdj * wAdj;
            sW *= div2;

            w += BaseType::alpha * mW / sqrt(sW + eps);

            return lossLinkGrad;*/
        }
    }
}
