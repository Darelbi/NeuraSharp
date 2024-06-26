﻿#region copyright
/*
 * MIT License
 * 
 * Copyright (c) 2020-2021 WhiteBlackGoose
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
     Original Project: https://github.com/Darelbi/NeuraSharp/ */
#endregion


using System.Numerics;

namespace GenericTensor.Core
{
    internal static class SquareMatrixFactory<T> where T:INumber<T>
    {
        // [0] is 1x1 matrix, [1] is 2x2 matrix, etc.
        static readonly List<GenTensor<T>> tensorTempFactorySquareMatrices = new List<GenTensor<T>>();

        internal static GenTensor<T> GetMatrix(int diagLength)
        {
            if (diagLength >= tensorTempFactorySquareMatrices.Count + 1)
                lock (tensorTempFactorySquareMatrices)
                    if (diagLength >= tensorTempFactorySquareMatrices.Count + 1)
                        for (int i = tensorTempFactorySquareMatrices.Count + 1; i <= diagLength; i++)
                            tensorTempFactorySquareMatrices.Add(new GenTensor<T>(i, i));
            return tensorTempFactorySquareMatrices[diagLength - 1];
        }
    }
}
