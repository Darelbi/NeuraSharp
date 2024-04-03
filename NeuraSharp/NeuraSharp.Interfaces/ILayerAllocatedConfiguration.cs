/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface ILayerAllocatedConfiguration<T> 
        : ILayerAllocatedVariables<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void SetLayer(int layerIndex0based);
    }
}
