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
using NeuraSharp.UnitTest.GenericTensor.TestUtils;
using System;
using System.Numerics;

namespace UnitTests
{
    [TestClass]
    public class Inverse
    {
        public Inverse()
        {
            
        }

        [TestMethod]
        public void Test1()
        {
            var A = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -2, 5},
                {2,  8, 7}
            });
            var B = A.Copy();
            B.InvertMatrix();

            TensorEquals.AssertTensorEquals(
                GenTensor<float>.CreateIdentityMatrix(3),
                GenTensor<float>.MatrixMultiply(A, B)
            );
        }

        [TestMethod]
        public void Test2()
        {
            var A = GenTensor<float>.CreateMatrix(new float[,]
            {
                {1, 2},
                {3, 4}
            });
            var B = A.Copy();
            B.InvertMatrix();
            Assert.AreEqual(
                GenTensor<float>.CreateIdentityMatrix(2),
                GenTensor<float>.MatrixMultiply(A, B)
            );
        }

        [TestMethod]
        public void Division()
        {
            var A = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -1, 5},
                {2,  8, 7}
            });

            var B = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -1, 5},
                {2,  8, 7}
            });

            var res = GenTensor<float>.MatrixDivide(A, B);
            var result = GenTensor<float>.MatrixMultiply(res, B);

            TensorEquals.AssertTensorEquals(A, result);
        }

        [TestMethod]
        public void DivisionDouble()
        {
            var A = GenTensor<double>.CreateMatrix(new double[,]
            {
                {6,  1, 1},
                {4, -1, 5},
                {2,  8, 7}
            });

            var B = GenTensor<double>.CreateMatrix(new double[,]
            {
                {6,  1, 1},
                {4, -1, 5},
                {2,  8, 7}
            });

            var res = GenTensor<double>.MatrixDivide(A, B);
            var result = GenTensor<double>.MatrixMultiply(res, B);

            TensorEquals.AssertTensorEquals(A, result);
        }

        //[TestMethod]
        //public void DivisionComplex()
        //{
        //    var A = GenTensor<Complex>.CreateMatrix(new Complex[,]
        //    {
        //        {6,  1, 1},
        //        {4, -2, 5},
        //        {2,  8, 7}
        //    });

        //    var B = GenTensor<Complex, ComplexWrapper>.CreateMatrix(new Complex[,]
        //    {
        //        {6,  1, 1},
        //        {4, -1, 5},
        //        {2,  8, 7}
        //    });

        //    var res = GenTensor<Complex, ComplexWrapper>.MatrixDivide(A, B);
        //    AreNonIntegerTensorEqual(
        //        GenTensor<Complex, ComplexWrapper>.MatrixMultiply(res, B),
        //        A
        //        );
        //}

        [TestMethod]
        public void TensorDivision()
        {
            var A = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -2, 5},
                {2,  8, 7}
            });

            var B = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -1, 5},
                {2,  8, 7}
            });

            var T1 = GenTensor<float>.Stack(A, B);
            var T2 = GenTensor<float>.Stack(B, A);

            var res = GenTensor<float>.TensorMatrixDivide(T1, T2);

            // OK THIS TEST FAILS BECAUSE THE DATA ARRAY IS RECYCLED IN GET SUBTENSOR)
            TensorEquals.AssertTensorEquals(res.GetSubtensor(0).Copy(), GenTensor<float>.MatrixDivide(A, B));
            TensorEquals.AssertTensorEquals(res.GetSubtensor(1).Copy(), GenTensor<float>.MatrixDivide(B, A));
        }

        [TestMethod]
        public void TensorMatrixInverse()
        {
            var A = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -2, 5},
                {2,  8, 7}
            });

            var B = GenTensor<float>.CreateMatrix(new float[,]
            {
                {6,  1, 1},
                {4, -2, 5},
                {2,  8, 7}
            });

            var T = GenTensor<float>.Stack(A, B);
            var K = T.Copy();
            T.TensorMatrixInvert();

            TensorEquals.AssertTensorEquals(
                GenTensor<float>.CreateIdentityTensor(T.Shape.SubShape(0, 2).ToArray(), 3),
                GenTensor<float>.TensorMatrixMultiply(T, K));
        }

        //internal void AreNonIntegerTensorEqual<T>(GenTensor<T> expected, GenTensor<T> actual) where T:INumber<T>
        //{
        //    var diff = GenTensor<T>.PiecewiseSubtract(expected, actual);
        //    var sum = default(TWrapper).CreateZero();
        //    foreach (var (_, value) in diff.Iterate())
        //        sum = default(TWrapper).Add(sum, default(TWrapper).Multiply(value, value));
        //    float abs;
        //    if (typeof(T) == typeof(float))
        //        abs = Math.Abs((float)(object)sum);
        //    else if (typeof(T) == typeof(double))
        //        abs = (float)Math.Abs((double)(object)sum);
        //    else if (typeof(T) == typeof(Complex))
        //        abs = (float)Complex.Abs((Complex)(object)sum);
        //    else
        //        throw new NotSupportedException();
        //    Assert.IsTrue(abs < 0.01f, $"Error: {diff}\nExpected: {expected}\nActual: {actual}");
        //}

        //[TestMethod]
        //public void DivisionGWFloat()
        //{
        //    var A = GenTensor<float>.CreateMatrix(new float[,]
        //    {
        //        {6,  1, 1},
        //        {4, -2, 5},
        //        {2,  8, 7}
        //    });

        //    var B = GenTensor<float>.CreateMatrix(new float[,]
        //    {
        //        {6,  1, 1},
        //        {4, -1, 5},
        //        {2,  8, 7}
        //    });

        //    var res = GenTensor<float>.MatrixDivide(A, B);
        //    AreNonIntegerTensorEqual(
        //        GenTensor<float>.MatrixMultiply(res, B),
        //        A
        //        );
        //}

        //[TestMethod]
        //public void DivisionGWDouble()
        //{
        //    var A = GenTensor<double, GenericWrapper<double>>.CreateMatrix(new double[,]
        //    {
        //        {6,  1, 1},
        //        {4, -2, 5},
        //        {2,  8, 7}
        //    });

        //    var B = GenTensor<double, GenericWrapper<double>>.CreateMatrix(new double[,]
        //    {
        //        {6,  1, 1},
        //        {4, -1, 5},
        //        {2,  8, 7}
        //    });

        //    var res = GenTensor<double, GenericWrapper<double>>.MatrixDivide(A, B);
        //    AreNonIntegerTensorEqual(
        //        GenTensor<double, GenericWrapper<double>>.MatrixMultiply(res, B),
        //        A
        //        );
        //}

        //[TestMethod]
        //public void DivisionGWComplex()
        //{
        //    var A = GenTensor<Complex, GenericWrapper<Complex>>.CreateMatrix(new Complex[,]
        //    {
        //        {6,  1, 1},
        //        {4, -2, 5},
        //        {2,  8, 7}
        //    });

        //    var B = GenTensor<Complex, GenericWrapper<Complex>>.CreateMatrix(new Complex[,]
        //    {
        //        {6,  1, 1},
        //        {4, -1, 5},
        //        {2,  8, 7}
        //    });

        //    var res = GenTensor<Complex, GenericWrapper<Complex>>.MatrixDivide(A, B);
        //    AreNonIntegerTensorEqual(
        //        GenTensor<Complex, GenericWrapper<Complex>>.MatrixMultiply(res, B),
        //        A
        //        );
        //}

        //[TestMethod]
        //public void DivisionGWString()
        //{
        //    var A = GenTensor<string>.CreateMatrix(new string[,]
        //    {
        //        {"6", " 1", "1"},
        //        {"4", "-2", "5"},
        //        {"2", " 8", "7"}
        //    });

        //    var B = GenTensor<string>.CreateMatrix(new string[,]
        //    {
        //        {"6", " 1", "1"},
        //        {"4", "-1", "5"},
        //        {"2", " 8", "7"}
        //    });

        //    Assert.ThrowsException<NotSupportedException>(() => GenTensor<string>.MatrixDivide(A, B));
        //}



        [TestMethod]
        public void InverseIssue22()
        {
            var a = GenTensor<int>.CreateMatrix(new [,]
                {
                    { 1, 0, 70 },
                    { 0, 1,  0 },
                    { 0, 0,  1 }
                }
            );
            a.InvertMatrix();
            Assert.AreEqual(GenTensor<int>.CreateMatrix(new [,]
                {
                    { 1, 0, -70 },
                    { 0, 1,  0 },
                    { 0, 0,  1 }
                }
            ), a);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void TestInverse(int size)
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    if (x == y)
                        continue;
                    var actual = GenTensor<int>.CreateIdentityMatrix(size);
                    actual[x, y] = 2007;
                    actual.InvertMatrix();

                    var expected = GenTensor<int>.CreateIdentityMatrix(size);
                    expected[x, y] = -2007;

                    Assert.AreEqual(expected, actual);
                }
        }

        [TestMethod]
        public void TestInverseOfOneMatrix()
        {
            var m = GenTensor<double>.CreateMatrix(new[,]{{-0.25d}});
            m.InvertMatrix();
            Assert.AreEqual(GenTensor<double>.CreateMatrix(new[,]{{-4d}}), m);
        }

        [TestMethod]
        public void TestAdjointOfOneMatrix()
        {
            var m = GenTensor<double>.CreateMatrix(new[,]{{1d}});
            Assert.AreEqual(GenTensor<double>.CreateMatrix(new[,]{{1d}}), m.Adjoint());
        }
    }
}
