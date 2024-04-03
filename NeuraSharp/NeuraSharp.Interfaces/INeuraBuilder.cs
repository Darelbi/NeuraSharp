/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuraBuilder<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        INeuraNetwork<T> Compile();
    }
}
