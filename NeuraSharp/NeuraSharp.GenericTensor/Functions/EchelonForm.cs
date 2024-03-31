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
    internal static class EchelonFormExtensions
    {
        internal static GenTensor<T> SafeDivisionToSimple<T>(
            this GenTensor<EchelonForm<T>.SafeDivisionWrapper<T>,
                EchelonForm<T>.WrapperSafeDivisionWrapper<T>> t)
            where T : INumber<T>
            => GenTensor<T>.CreateMatrix(t.Shape[0], t.Shape[1], (x, y) => t.GetValueNoCheck(x, y).Count());

        internal static
            GenTensor<EchelonForm<T>.SafeDivisionWrapper<T>,
                EchelonForm<T>.WrapperSafeDivisionWrapper<T>>
            SimpleToSafeDivision<T>(this GenTensor<T> t) where T : INumber<T>
            => GenTensor<EchelonForm<T>.SafeDivisionWrapper<T>,
                    EchelonForm<T>.WrapperSafeDivisionWrapper<T>>
                .CreateMatrix(t.Shape[0], t.Shape[1],
                    (x, y) => new EchelonForm<T>.SafeDivisionWrapper<T>(t.GetValueNoCheck(x, y))
                );
    }

    internal static class EchelonForm<T> where T : INumber<T>
    {
        #region Gaussian elimination safe division

        internal struct SafeDivisionWrapper<W> where W : INumber<W>
        {
            internal W num;
            internal W den;

            public SafeDivisionWrapper(W val)
            {
                num = val;
                den = W.One;
            }

            public SafeDivisionWrapper(W num, W den)
            {
                this.num = num;
                this.den = den;
            }

            public W Count() => num / den;
        }


        /// <summary>
        /// Store divisions as numerator / denominator, allowing increase of precision
        /// In example summing (a/b) + (c/d) results in (a*d+c*b)/(b*d)
        /// : TODO: Make it derive from INumber => it is a very interesting solution
        /// </summary>
        /// <typeparam name="W"></typeparam>
        internal struct WrapperSafeDivisionWrapper<W>
            where W : INumber<W>
        {
            public SafeDivisionWrapper<W> Add(SafeDivisionWrapper<W> a, SafeDivisionWrapper<W> b)
            {
                return new SafeDivisionWrapper<W>(

                    a.num * b.den + b.num * a.den,
                   a.den * b.den);
            }

            public SafeDivisionWrapper<W> Subtract(SafeDivisionWrapper<W> a, SafeDivisionWrapper<W> b)
            {
                return new SafeDivisionWrapper<W>(
                    a.num * b.den - b.num * a.den,
                    a.den * b.den);
            }

            public SafeDivisionWrapper<W> Multiply(SafeDivisionWrapper<W> a, SafeDivisionWrapper<W> b)
            {
                return new SafeDivisionWrapper<W>(a.num * b.num, a.den * b.den);
            }

            public SafeDivisionWrapper<W> Negate(SafeDivisionWrapper<W> a)
            {
                return new SafeDivisionWrapper<W>(-a.num, a.den);
            }

            public SafeDivisionWrapper<W> Divide(SafeDivisionWrapper<W> a, SafeDivisionWrapper<W> b)
            {
                return new SafeDivisionWrapper<W>(a.num * b.den, a.den * b.num);
            }

            public SafeDivisionWrapper<W> CreateOne()
            {
                return new SafeDivisionWrapper<W>(W.One);
            }

            public SafeDivisionWrapper<W> CreateZero()
            {
                return new SafeDivisionWrapper<W>(W.Zero);
            }

            public SafeDivisionWrapper<W> Copy(SafeDivisionWrapper<W> a)
            {
                return new SafeDivisionWrapper<W>(a.num,a.den);
            }

            public SafeDivisionWrapper<W> Forward(SafeDivisionWrapper<W> a)
            {
                return a;
            }

            public bool AreEqual(SafeDivisionWrapper<W> a, SafeDivisionWrapper<W> b)
            {
                // return a.num== b.num && a.den== b.den ;
                // let's improve equality: a/b == c/d <=>  a*d = c*b
                return (a.num * b.den) - (b.num * a.den) == W.Zero;
            }

            public bool IsZero(SafeDivisionWrapper<W> a)
            {
                return a.num == W.Zero;
            }

            public string ToString(SafeDivisionWrapper<W> a)
            {
                return a.num + " / " + a.den;
            }

            public byte[] Serialize(SafeDivisionWrapper<W> a)
            {
                return Serializer<W>.GetBytesGeneric(a.num).Concat(Serializer<W>.GetBytesGeneric(a.den)).ToArray();
            }

            public SafeDivisionWrapper<W> Deserialize(byte[] data)
            {
                var half = data.Length / 2;
                var part1 = data.Take(half).ToArray();
                var part2 = data.Skip(half).Take(half).ToArray();

                return new SafeDivisionWrapper<W>(  Serializer<W>.DeserializeGeneric(part1),
                                                     Serializer<W>.DeserializeGeneric(part2)
                                                    );
            }
        }

        internal static GenTensor<SafeDivisionWrapper<T>> // OK NOW I NEED IT TO BE a INumber
            InnerGaussianEliminationSafeDivision(GenTensor<T> t, int m, int n, int[]? permutations, out int swapCount)
        {
            var elemMatrix = t.SimpleToSafeDivision();
            swapCount = 0;
            EchelonForm<SafeDivisionWrapper<T>, WrapperSafeDivisionWrapper<T>>
                .InnerGaussianEliminationSimple(elemMatrix, 0, permutations, ref swapCount);
            return elemMatrix;
        }

        public static GenTensor<T> RowEchelonFormSafeDivision(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            var wrp = InnerGaussianEliminationSafeDivision(t, t.Shape[0], t.Shape[1], null, out _);
            return wrp.SafeDivisionToSimple();
        }

        internal static void InnerGaussianEliminationSimpleDiscardSwapCount(GenTensor<T> t, int off)
        {
            var intoNowhere = 0;
            InnerGaussianEliminationSimple(t, off, null, ref intoNowhere);
        }

        internal static void InnerGaussianEliminationSimple(GenTensor<T> t, int off, int[]? permutations, ref int swapCount)
        {
            // Here we are sticking to the algorithm,
            // provided here: https://www.math.purdue.edu/~shao92/documents/Algorithm%20REF.pdf
            // We can afford it, since it is implemented with tail-recursion.


            // II. No non-zero columns => the matrix is zero
            if (LeftmostNonZeroColumn(t, off) is not var (columnId, pivotId))
                return;


            // III. If the first non-zero element in a column is not in the first row,
            // we swap those rows to make it in the first row
            if (pivotId != off)
            {
                t.RowSwap(off, pivotId);
                swapCount++;

                if (permutations is not null)
                    (permutations[off], permutations[pivotId]) = (permutations[pivotId], permutations[off]);
            }


            // IV. Now we shall go over all rows below off to make their
            // first element equal 0
            var pivotValue = t.GetValueNoCheck(off, columnId);
            for (int r = off + 1; r < t.Shape[0]; r++)
                if (!default(TWrapper).IsZero(t.GetValueNoCheck(r, columnId)))
                {
                    var currElement = t.GetValueNoCheck(r, columnId);
                    t.RowSubtract(r, off, default(TWrapper).Divide(currElement, pivotValue));
                }


            // VI. Let us apply the algorithm for the inner matrix
            InnerGaussianEliminationSimple(t, off + 1, permutations, ref swapCount);


            static int? NonZeroColumn(GenTensor<T> t, int c, int off)
            {
                for (int i = off; i < t.Shape[0]; i++)
                    if (!default(TWrapper).IsZero(t.GetValueNoCheck(i, c)))
                        return i;
                return null;
            }


            static (int columnId, int pivotId)? LeftmostNonZeroColumn(GenTensor<T> t, int off)
            {
                for (int c = off; c < t.Shape[1]; c++)
                    if (NonZeroColumn(t, c, off) is { } nonZero)
                        return (c, nonZero);
                return null;
            }
        }

        public static GenTensor<T> RowEchelonFormSimple(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            var res = t.Copy(copyElements: false);
            InnerGaussianEliminationSimpleDiscardSwapCount(res, 0);
            return res;
        }

        public static (GenTensor<T>, int[]) RowEchelonFormPermuteSimple(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            var res = t.Copy(copyElements: false);
            var permute = new int[t.Shape[0]];
            for (var i = 0; i < permute.Length; i++) permute[i] = i + 1;

            var _ = 0;
            InnerGaussianEliminationSimple(res, 0, permute, ref _);
            return (res, permute);
        }

        public static (GenTensor<T>, int[]) RowEchelonFormPermuteSafeDivision(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            var permutations = new int[t.Shape[0]];
            for (var i = 0; i < permutations.Length; i++) permutations[i] = i + 1;

            var wrp = InnerGaussianEliminationSafeDivision(t, t.Shape[0], t.Shape[1], permutations, out _);
            return (wrp.SafeDivisionToSimple(), permutations);
        }

        #endregion

        #region Row echelon form leading ones

        private static GenTensor<T> InnerRowEchelonFormLeadingOnes(GenTensor<T> t)
        {
            var rowForm = t.Copy(copyElements: false);
            InnerGaussianEliminationSimpleDiscardSwapCount(rowForm, 0);
            for (int r = 0; r < t.Shape[0]; r++)
                if (rowForm.RowGetLeadingElement(r) is { } leading)
                    rowForm.RowMultiply(r, default(TWrapper).Divide(default(TWrapper).CreateOne(), leading.value));
            return rowForm;
        }

        public static GenTensor<T> RowEchelonFormLeadingOnesSimple(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            return InnerRowEchelonFormLeadingOnes(t);
        }

        public static GenTensor<T> RowEchelonFormLeadingOnesSafeDivision(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            return EchelonForm<SafeDivisionWrapper<T>, WrapperSafeDivisionWrapper<T>>
                .InnerRowEchelonFormLeadingOnes(t.SimpleToSafeDivision()).SafeDivisionToSimple();
        }

        #endregion

        #region Reduced row echelon form

        private static GenTensor<T> InnerReducedRowEchelonForm(GenTensor<T> t, int[]? permutations, out int swapCount)
        {
            var upper = t.Copy(copyElements: false);
            swapCount = 0;
            InnerGaussianEliminationSimple(upper, 0, permutations, ref swapCount);
            for (int r = t.Shape[0] - 1; r >= 0; r--)
            {
                if (upper.RowGetLeadingElement(r) is not { } leading)
                    continue;
                for (int i = 0; i < r; i++)
                    upper.RowSubtract(i, r,
                        default(TWrapper).Divide(upper.GetValueNoCheck(i, leading.index), leading.value));

                upper.RowMultiply(r, default(TWrapper).Divide(default(TWrapper).CreateOne(), leading.value));
            }

            return upper;
        }

        public static GenTensor<T> ReducedRowEchelonFormSimple(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            return InnerReducedRowEchelonForm(t, null, out _);
        }

        public static GenTensor<T> ReducedRowEchelonFormSafeDivision(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif
            return EchelonForm<SafeDivisionWrapper<T>, WrapperSafeDivisionWrapper<T>>
                .InnerReducedRowEchelonForm(t.SimpleToSafeDivision(), null, out var _).SafeDivisionToSimple();
        }

        public static (GenTensor<T> result, int[] permutations) ReducedRowEchelonFormPermuteSafeDivision(GenTensor<T> t)
        {
#if ALLOW_EXCEPTIONS
            if (!t.IsMatrix)
                throw new InvalidShapeException("this should be matrix");
#endif

            var permutation = new int[t.Shape[0]];
            for (var i = 0; i < permutation.Length; i++) permutation[i] = i + 1;

            return (EchelonForm<SafeDivisionWrapper<T>, WrapperSafeDivisionWrapper<T>>
                    .InnerReducedRowEchelonForm(t.SimpleToSafeDivision(), permutation, out var _).SafeDivisionToSimple(),
                permutation);
        }

        #endregion
    }
}