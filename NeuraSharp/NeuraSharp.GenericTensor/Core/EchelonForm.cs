#region copyright
/*
 * MIT License
 * 
 * Copyright (c) 2020-2021 WhiteBlackGoose, (c) 2024 Dario Oliveri
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
using GenericTensor.Functions;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace NeuraSharp.GenericTensor.Core
{
    public class EchelonForm<T> : INumber<EchelonForm<T>>
        where T : INumber<T>
    {
        private T num;
        private T den;

        public EchelonForm(T val)
        {
            num = val;
            den = T.One;
        }

        public EchelonForm(T num, T den)
        {
            this.num = num;
            this.den = den;
        }

        #region to import

        public string ToString(EchelonForm<T> a)
        {
            return a.num + " / " + a.den;
        }

        public byte[] Serialize(EchelonForm<T> a)
        {
            return Serializer<T>.GetBytesGeneric(a.num).Concat(Serializer<T>.GetBytesGeneric(a.den)).ToArray();
        }

        public EchelonForm<T> Deserialize(byte[] data)
        {
            var half = data.Length / 2;
            var part1 = data.Take(half).ToArray();
            var part2 = data.Skip(half).Take(half).ToArray();

            return new EchelonForm<T>(Serializer<T>.DeserializeGeneric(part1),
                                                 Serializer<T>.DeserializeGeneric(part2)
                                                );
        }
        #endregion

        public T Count() => num / den;

        public static EchelonForm<T> One => new EchelonForm<T>(T.One);

        public static int Radix => throw new NotImplementedException();

        public static EchelonForm<T> Zero => new EchelonForm<T>(T.Zero);

        public static EchelonForm<T> AdditiveIdentity => Zero;

        public static EchelonForm<T> MultiplicativeIdentity => One;

        public static EchelonForm<T> Abs(EchelonForm<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsCanonical(EchelonForm<T> value)
        {
            return value.den == T.One;
        }

        public static bool IsComplexNumber(EchelonForm<T> value)
        {
            return T.IsComplexNumber(value.num);
        }

        public static bool IsEvenInteger(EchelonForm<T> value)
        {
            var val = (value.num / value.den) * value.den;

            return (val == value.num && T.IsEvenInteger(value.den))
                || (val == value.num && T.IsEvenInteger(value.num))
                ;
        }

        public static bool IsFinite(EchelonForm<T> value)
        {
            if (value.den == T.Zero && value.num != T.Zero)
                return false;

            return T.IsFinite(value.num);
        }

        public static bool IsImaginaryNumber(EchelonForm<T> value)
        {
            return T.IsImaginaryNumber(value.num);
        }

        public static bool IsInfinity(EchelonForm<T> value)
        {
            return T.IsInfinity(value.num) && !IsNaN(value) && value.den == T.Zero;
        }

        public static bool IsInteger(EchelonForm<T> value)
        {
            return T.IsInteger(value.num) && (value.num / value.den) * value.den == value.num;
        }

        public static bool IsNaN(EchelonForm<T> value)
        {
            return (T.IsInfinity(value.num) && T.IsInfinity(value.den))
                || (T.IsInfinity(value.num) && value.den == T.Zero)
                || T.IsNaN(value.num) || T.IsNaN(value.den);
        }

        public static bool IsNegative(EchelonForm<T> value)
        {
            return T.Sign(value.num) * T.Sign(value.den) < 0;
        }

        public static bool IsNegativeInfinity(EchelonForm<T> value)
        {
            return T.IsNegativeInfinity(value.num) && !IsNaN(value);
        }

        public static bool IsNormal(EchelonForm<T> value)
        {
            return T.IsNormal(value.num) && T.IsNormal(value.den);
        }

        public static bool IsOddInteger(EchelonForm<T> value)
        {
            var val = (value.num / value.den) * value.den;

            return (val == value.num && !T.IsEvenInteger(value.den))
                || (val == value.num && !T.IsEvenInteger(value.num))
                ;
        }

        public static bool IsPositive(EchelonForm<T> value)
        {
            return T.Sign(value.num) * T.Sign(value.den) > 0;
        }

        public static bool IsPositiveInfinity(EchelonForm<T> value)
        {
            return T.IsPositiveInfinity(value.num) && !IsNaN(value);
        }

        public static bool IsRealNumber(EchelonForm<T> value)
        {
            return !IsNaN(value) && T.IsRealNumber(value.num);
        }

        public static bool IsSubnormal(EchelonForm<T> value)
        {
            return T.IsSubnormal(value.num) || T.IsSubnormal(value.den);
        }

        public static bool IsZero(EchelonForm<T> value)
        {
            return value.num == T.Zero;
        }

        public static EchelonForm<T> MaxMagnitude(EchelonForm<T> x, EchelonForm<T> y)
        {
            if (IsNaN(x))
                return x;
            if (IsNaN(y))
                return y;

            return new EchelonForm<T>( T.Max( T.Abs(x.num/x.den), T.Abs(y.num/y.den)) ); // todo: improve precision
        }

        public static EchelonForm<T> MaxMagnitudeNumber(EchelonForm<T> x, EchelonForm<T> y)
        {
            if (IsNaN(x))
                return y;
            if (IsNaN(y))
                return x;

            return new EchelonForm<T>(T.Max(T.Abs(x.num / x.den), T.Abs(y.num / y.den))); // todo: improve precision
        }

        public static EchelonForm<T> MinMagnitude(EchelonForm<T> x, EchelonForm<T> y)
        {
            if (IsNaN(x))
                return x;
            if (IsNaN(y))
                return y;

            return new EchelonForm<T>(T.Min(T.Abs(x.num / x.den), T.Abs(y.num / y.den))); // todo: improve precision
        }

        public static EchelonForm<T> MinMagnitudeNumber(EchelonForm<T> x, EchelonForm<T> y)
        {
            if (IsNaN(x))
                return y;
            if (IsNaN(y))
                return x;

            return new EchelonForm<T>(T.Min(T.Abs(x.num / x.den), T.Abs(y.num / y.den))); // todo: improve precision
        }

        public static EchelonForm<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static EchelonForm<T> Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static EchelonForm<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static EchelonForm<T> Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(EchelonForm<T>? other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(EchelonForm<T>? other)
        {
            return this.num * other.den == this.den * other.num;
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<EchelonForm<T>>.TryConvertFromChecked<TOther>(TOther value, out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<EchelonForm<T>>.TryConvertFromSaturating<TOther>(TOther value, out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<EchelonForm<T>>.TryConvertFromTruncating<TOther>(TOther value, out EchelonForm<T> result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<EchelonForm<T>>.TryConvertToChecked<TOther>(EchelonForm<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<EchelonForm<T>>.TryConvertToSaturating<TOther>(EchelonForm<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        static bool INumberBase<EchelonForm<T>>.TryConvertToTruncating<TOther>(EchelonForm<T> value, out TOther result)
        {
            throw new NotImplementedException();
        }

        public static EchelonForm<T> operator +(EchelonForm<T> value)
        {
            return new EchelonForm<T>(+value.num, value.den);
        }

        public static EchelonForm<T> operator +(EchelonForm<T> a, EchelonForm<T> b)
        {
            return new EchelonForm<T>(

                   a.num * b.den + b.num * a.den,
                  a.den * b.den);
        }

        public static EchelonForm<T> operator -(EchelonForm<T> value)
        {
            return new EchelonForm<T>(-value.num, value.den);
        }

        public static EchelonForm<T> operator -(EchelonForm<T> a, EchelonForm<T> b)
        {
            return new EchelonForm<T>(
                    a.num * b.den - b.num * a.den,
                    a.den * b.den);
        }

        public static EchelonForm<T> operator ++(EchelonForm<T> value)
        {
            return value + EchelonForm<T>.One;
        }

        public static EchelonForm<T> operator --(EchelonForm<T> value)
        {
            return value - EchelonForm<T>.One;
        }

        public static EchelonForm<T> operator *(EchelonForm<T> a, EchelonForm<T> b)
        {
            return new EchelonForm<T>(a.num * b.num, a.den * b.den);
        }

        public static EchelonForm<T> operator /(EchelonForm<T> a, EchelonForm<T> b)
        {
            return new EchelonForm<T>(a.num * b.den, a.den * b.num);
        }

        public static EchelonForm<T> operator %(EchelonForm<T> left, EchelonForm<T> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(EchelonForm<T>? left, EchelonForm<T>? right)
        {
            return left.num * right.den == left.den * right.num;
        }

        public static bool operator !=(EchelonForm<T>? left, EchelonForm<T>? right)
        {
            return left.num * right.den != left.den * right.num;
        }

        public static bool operator <(EchelonForm<T> left, EchelonForm<T> right)
        {
            return left.num / left.den < right.num / right.den;
        }

        public static bool operator >(EchelonForm<T> left, EchelonForm<T> right)
        {
            return left.num / left.den > right.num / right.den;
        }

        public static bool operator <=(EchelonForm<T> left, EchelonForm<T> right)
        {
            return left.num / left.den <= right.num / right.den;
        }

        public static bool operator >=(EchelonForm<T> left, EchelonForm<T> right)
        {
            return left.num / left.den >= right.num / right.den;
        }

        internal static GenTensor<EchelonForm<T>>
            InnerGaussianEliminationSafeDivision(GenTensor<T> t, int m, int n, int[]? permutations, out int swapCount)
        {
            var elemMatrix = t.SimpleToSafeDivision();
            swapCount = 0;
            EchelonForm<EchelonForm<T>>
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
                if (!T.IsZero(t.GetValueNoCheck(r, columnId)))
                {
                    var currElement = t.GetValueNoCheck(r, columnId);
                    t.RowSubtract(r, off, currElement/ pivotValue);
                }


            // VI. Let us apply the algorithm for the inner matrix
            InnerGaussianEliminationSimple(t, off + 1, permutations, ref swapCount);


            static int? NonZeroColumn(GenTensor<T> t, int c, int off)
            {
                for (int i = off; i < t.Shape[0]; i++)
                    if (!T.IsZero(t.GetValueNoCheck(i, c)))
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

        private static GenTensor<T> InnerRowEchelonFormLeadingOnes(GenTensor<T> t)
        {
            var rowForm = t.Copy(copyElements: false);
            InnerGaussianEliminationSimpleDiscardSwapCount(rowForm, 0);
            for (int r = 0; r < t.Shape[0]; r++)
                if (rowForm.RowGetLeadingElement(r) is { } leading)
                    rowForm.RowMultiply(r, T.One/ leading.value);
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
            return EchelonForm<EchelonForm<T>> // TODO: MOVE OUTSIDE SINCE THIS SYNTAX IS STRANGE EVEN IF LEAD TO CORRECT RESULT
                .InnerRowEchelonFormLeadingOnes(t.SimpleToSafeDivision()).SafeDivisionToSimple();
        }

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
                        upper.GetValueNoCheck(i, leading.index)/ leading.value);

                upper.RowMultiply(r, T.One/ leading.value);
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
            return EchelonForm<EchelonForm<T>>
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

            return (EchelonForm<EchelonForm<T>>
                    .InnerReducedRowEchelonForm(t.SimpleToSafeDivision(), permutation, out var _).SafeDivisionToSimple(),
                permutation);
        }
    }
}
