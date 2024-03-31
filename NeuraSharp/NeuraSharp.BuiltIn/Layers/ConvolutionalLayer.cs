using NeuraSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeuraSharp.BuiltIn.Layers
{
    public class ConvolutionalLayer<T> : INeuralLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[][] Weights { get; set; }
        public T[] Gradients { get; set; }
        public T[] Derivates { get; set; }
        public T[] Outputs { get; set; }
        public T[] TotalGradients { get; set; }
        public T[] Biases { get; set; }

        public ConvolutionalLayer(
            int xInputSize, int yInputSize,
            int kernelSize, int kernelStride, int dilation,
            IActivationFunction<T> activation)
        {
            this.activation = activation;

            Outputs = new T[outputs];
            Derivates = new T[outputs];
            Gradients = new T[outputs];
            TotalGradients = new T[outputs];
            Biases = new T[outputs];

            Weights = new T[outputs][];

            for (int i = 0; i < outputs; i++)
            {
                Weights[i] = new T[inputs];
            }
        }

        public void ForwardFrom(T[] previousLayerOutput, bool computeDerivates)
        {
            throw new NotImplementedException();
        }

        public IActivationFunction<T> GetActivationFunction()
        {
            throw new NotImplementedException();
        }

        public IRegularizationAlgorithm<T>[] GetRegularizers()
        {
            throw new NotImplementedException();
        }
    }
}
