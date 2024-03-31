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
 */
#endregion


using GenericTensor.Core;
using System.Numerics;

namespace GenericTensor.Functions
{
    internal static class Determinant<T> where T : INumber<T>
    {
        #region Matrix Determinant
        #region Laplace
        internal static T DeterminantLaplace(GenTensor<T> t, int diagLength)
        {
            if (diagLength == 1)
                return t.GetValueNoCheck(0, 0);
            var det = T.Zero;
            var sign = T.One;
            var temp = SquareMatrixFactory<T>.GetMatrix(diagLength - 1);
            for (int i = 0; i < diagLength; i++)
            {
                Inversion<T>.GetCofactorMatrix(t, temp, 0, i, diagLength);
                det = det + sign * t.GetValueNoCheck(0, i) *
                            DeterminantLaplace(temp, diagLength - 1);

                sign = -sign;
            }
            return det;
        }

        public static T DeterminantLaplace(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("Determinant function should be only called from a matrix");
            if (t.Shape[0] != t.Shape[1])
                throw new InvalidShapeException("Matrix should be square");
#endif
            return DeterminantLaplace(t, t.Shape[0]);
        }
        #endregion

        #region Gaussian


        public static T DeterminantGaussianSafeDivision(GenTensor<T> t)
            => DeterminantGaussianSafeDivision(t, t.Shape[0]);

        internal static T DeterminantGaussianSafeDivision(GenTensor<T> t, int diagLength)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
            if (t.Shape[0] != t.Shape[1])
                throw new InvalidShapeException("this should be square matrix");
#endif

            if (t.Shape[0] == 1)
                return t.GetValueNoCheck(0, 0);

            var n = diagLength;
            var elemMatrix = EchelonForm<T>.InnerGaussianEliminationSafeDivision(t, n, n, null, out var swapCount);

            var det = default(EchelonForm<T>.WrapperSafeDivisionWrapper<T>).CreateOne();
            for (int i = 0; i < n; i++)
            {
                det = default(EchelonForm<T>.WrapperSafeDivisionWrapper<T>).Multiply(det, elemMatrix.GetValueNoCheck(i, i));
            }

            if (default(TWrapper).IsZero(det.den))
                return default(TWrapper).CreateZero();
            return swapCount % 2 is 0 ? det.Count() : default(TWrapper).Negate(det.Count());
        }

        public static T DeterminantGaussianSimple(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
            if (t.Shape[0] != t.Shape[1])
                throw new InvalidShapeException("this should be square matrix");
#endif
            if (t.Shape[0] == 1)
                return t.GetValueNoCheck(0, 0);

            var n = t.Shape[0];

            var elemMatrix = t.Forward();
            for (int k = 1; k < n; k++)
                for (int j = k; j < n; j++)
                {
                    var m = default(TWrapper).Divide(
                        elemMatrix.GetValueNoCheck(j, k - 1),
                        elemMatrix.GetValueNoCheck(k - 1, k - 1)
                    );
                    for (int i = 0; i < n; i++)
                    {
                        var curr = elemMatrix.GetValueNoCheck(j, i);
                        elemMatrix.SetValueNoCheck(default(TWrapper).Subtract(
                            curr,
                            default(TWrapper).Multiply(
                                m,
                                elemMatrix.GetValueNoCheck(k - 1, i)
                            )
                        ), j, i);
                    }
                }

            var det = default(TWrapper).CreateOne();
            for (int i = 0; i < n; i++)
            {
                det = default(TWrapper).Multiply(det, elemMatrix.GetValueNoCheck(i, i));
            }

            return det;
        }
        #endregion
        #endregion

        #region Tensor Determinant

        public static GenTensor<T> TensorDeterminantLaplace(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            InvalidShapeException.NeedTensorSquareMatrix(t);
#endif

            var res = GenTensor<T>.CreateTensor(t.Shape.SubShape(0, 2),
                ind => t.GetSubtensor(ind).DeterminantLaplace());
            return res;
        }

        public static GenTensor<T> TensorDeterminantGaussianSafeDivision(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            InvalidShapeException.NeedTensorSquareMatrix(t);
#endif

            var res = GenTensor<T>.CreateTensor(t.Shape.SubShape(0, 2),
                ind => t.GetSubtensor(ind).DeterminantGaussianSafeDivision());
            return res;
        }

        public static GenTensor<T> TensorDeterminantGaussianSimple(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            InvalidShapeException.NeedTensorSquareMatrix(t);
#endif

            var res = GenTensor<T>.CreateTensor(t.Shape.SubShape(0, 2),
                ind => t.GetSubtensor(ind).DeterminantGaussianSimple());
            return res;
        }

        #endregion
    }
}
