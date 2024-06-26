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


using GenericTensor.Core;
using GenericTensor.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Concat
    {
        [TestMethod]
        public void Matrices()
        {
            var mat1 = GenTensor<int>.CreateMatrix(
                new[,]
                {
                    {1, 2},
                    {3, 4},
                    {5, 6}
                }
                );

            var mat2 = GenTensor<int>.CreateMatrix(
                new[,]
                {
                    {7, 8},
                    {9, 10},
                }
            );

            Assert.AreEqual(
                GenTensor<int>.CreateMatrix(new [,]
                {
                    {1, 2},
                    {3, 4},
                    {5, 6},
                    {7, 8},
                    {9, 10}
                }),
                GenTensor<int>.Concat(mat1, mat2)
                );
        }

        [TestMethod]
        public void Vecs()
        {
            var vec1 = GenTensor<int>.CreateVector(1, 2, 3);
            var vec2 = GenTensor<int>.CreateVector(4, 5);
            Assert.AreEqual(
                GenTensor<int>.CreateVector(1, 2, 3, 4, 5),
                GenTensor<int>.Concat(vec1, vec2)
                );
        }
    }
}
