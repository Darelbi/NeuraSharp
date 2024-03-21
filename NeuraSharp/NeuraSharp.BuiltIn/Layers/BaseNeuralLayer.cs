﻿using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Layers
{
    public class BaseNeuralLayer<T> : INeuralLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly IActivationFunction<T> activation;

        public T[] Outputs { get; set; }
        public T[] Derivates { get; set; }
        public T[] Errors { get; set; }
        public T[][] Weights { get; set; }
        public T[] Biases { get; set; }
        public T[] Gradients { get; set; }
        public T[] TotalGradients { get; set; }
        public T[] IterationDeltaWeight { get; set; }
        public T[] IterationDeltaBias { get; set; }

        public int Index { get; set; }

        public BaseNeuralLayer(IActivationFunction<T> activation)
        {
            this.activation = activation;
        }

        public IActivationFunction<T> GetActivationFunction()
        {
            // the function is layer specific that's why we need it there
            return activation;
        }

        public void Initialize(int index, int inputs, int outputs)
        {
            Index = index;
            Outputs = new T[outputs];
            Derivates = new T[outputs];
            Errors = new T[outputs];
            Gradients = new T[outputs];
            Biases = new T[outputs];

            Weights = new T[outputs][];

            for (int i = 0; i < outputs; i++)
            {
                Weights[i] = new T[inputs];
            }
        }
    }
}
