#region copyright

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


using GenericTensor.Core;
using NeuraSharp.GenericTensor.Core;
using System.Numerics;

namespace GenericTensor.Functions
{
    internal static class EchelonFormExtensions
    {
        internal static GenTensor<T> SafeDivisionToSimple<T>(
            this GenTensor<EchelonForm<T>> t)
            where T : INumber<T>
            => GenTensor<T>.CreateMatrix(t.Shape[0], t.Shape[1], (x, y) => t.GetValueNoCheck(x, y).Count());

        internal static
            GenTensor<EchelonForm<T>>
            SimpleToSafeDivision<T>(this GenTensor<T> t) where T : INumber<T>
            => GenTensor<EchelonForm<T>>
                .CreateMatrix(t.Shape[0], t.Shape[1],
                    (x, y) => new EchelonForm<T>(t.GetValueNoCheck(x, y))
                );
    }
}