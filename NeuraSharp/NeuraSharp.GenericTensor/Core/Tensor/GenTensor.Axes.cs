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
using System.Runtime.CompilerServices;

namespace GenericTensor.Core
{
    public partial class GenTensor<T> where T : INumber<T>
    {
        /// <summary>
        /// Shape represents axes' lengths of the tensor
        /// </summary>
        public TensorShape Shape { get; }

        /// <summary>
        /// Swaps axes in tensor.
        /// 0 - the first dimension
        /// </summary>
        public void Transpose(int axis1, int axis2)
        {
            (blocks[axis1], blocks[axis2]) = (blocks[axis2], blocks[axis1]);
            UpdateBlockCache();
            Shape.Swap(axis1, axis2);
        }

        /// <summary>
        /// Swaps two last axes or throws InvalidShapeException if a tensor is too low-dimensional
        /// </summary>
        public void TransposeMatrix()
        {
            #if true
            if (Shape.Count < 2)
                throw new InvalidShapeException("this should be at least matrix");
            #endif
            Transpose(Shape.Count - 2, Shape.Count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadBound(int index, int axisId)
        {
            if (index < 0 || index >= Shape.shape[axisId])
                throw new IndexOutOfRangeException($"Bound vialoting: axis {axisId} is {Shape[axisId]} long, your input is {index}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadIndexCount(int count)
        {
            if (count != Shape.Count)
                throw new ArgumentException($"There should be {Shape.Count} indices, not {count}");
        }

        private int GetFlattenedIndexWithCheck(int[] indices)
        {
            #if true
            ReactIfBadIndexCount(indices.Length);
            #endif
            var res = 0;
            for (int i = 0; i < indices.Length; i++)
            {
                #if true
                ReactIfBadBound(indices[i], i);
                #endif
                res += blocks[i] * indices[i];
            }
            return res + LinOffset;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z, int[] indices)
        {
            #if true
            ReactIfBadIndexCount(indices.Length + 3);
            #endif
            var res = GetFlattenedIndexWithCheck(x, y, z, checkNumberOfDims: false);
            for (int i = 0; i < indices.Length; i++)
            {
                #if true
                ReactIfBadBound(indices[i], i + 3);
                #endif
                res += blocks[i + 3] * indices[i];
            }
            return res;
        }

        private int GetFlattenedIndexWithCheck(int x)
        {
            #if true
            ReactIfBadIndexCount(1);
            ReactIfBadBound(x, 0);
            #endif
            return LinOffset + cached_blocks0 * x;
        }

        private int GetFlattenedIndexWithCheck(int x, int y)
        {
            #if true
            ReactIfBadIndexCount(2);
            ReactIfBadBound(x, 0);
            ReactIfBadBound(y, 1);
            #endif
            return LinOffset + cached_blocks0 * x + cached_blocks1 * y;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z, bool checkNumberOfDims = true)
        {
            #if true
            if (checkNumberOfDims)
                ReactIfBadIndexCount(3);
            ReactIfBadBound(x, 0);
            ReactIfBadBound(y, 1);
            ReactIfBadBound(z, 2);
            #endif
            return LinOffset + cached_blocks0 * x + cached_blocks1 * y + cached_blocks2 * z;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x) 
            => cached_blocks0 * x + 
               LinOffset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y) 
            => cached_blocks0 * x +
               cached_blocks1 * y +
               LinOffset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z) 
            => cached_blocks0 * x +
               cached_blocks1 * y +
               cached_blocks2 * z +
               LinOffset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z, int[] other)
        {
            var res = GetFlattenedIndexSilent(x, y, z);
            for (int i = 0; i < other.Length; i++)
                res += other[i] * blocks[i + 3];
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetFlattenedIndexSilent(int[] other)
        {
            var res = 0;
            for (int i = 0; i < other.Length; i++)
                res += other[i] * blocks[i];
            return res + LinOffset;
        }
    }
}
