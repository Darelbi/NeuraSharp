﻿/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface IBackwardInputLayer<T>
        : IGradientsLayer<T>, IWeightsLayer<T>
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        
    }
}
