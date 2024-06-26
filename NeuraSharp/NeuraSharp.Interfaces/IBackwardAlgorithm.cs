﻿/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces.Layers;
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IBackwardAlgorithm<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void Backward(IBackwardOutputLayer<T> iLayer, IBackwardInputLayer<T> iPlusOneLayer, T[] target);

        public void BackwardLast(IBackwardLastLayer<T> Llayer, T[] target);

        public ILossFunction<T> GetLossFunction();
    }
}
