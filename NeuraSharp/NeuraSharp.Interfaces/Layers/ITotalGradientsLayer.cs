/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface ITotalGradientsLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] TotalGradients { get; set; }
    }
}
