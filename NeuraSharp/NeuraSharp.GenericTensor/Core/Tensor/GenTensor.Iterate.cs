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


using HonkPerf.NET.Core;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GenericTensor.Core
{
    public partial class GenTensor<T> where T : INumber<T>
    {
        private void NextIndex(int[] indices, int id)
        {
            if (id == -1)
                return;
            indices[id]++;
            if (indices[id] == Shape[id])
            {
                indices[id] = 0;
                NextIndex(indices, id - 1);
            }
        }

        /// <summary>
        /// Similar to <see cref="List{T}.ForEach(Action{T})" />.
        /// </summary>
        /// <param name="iterator">
        /// Value function, which takes two parameters:
        /// index array and value of the tensor in this point.
        /// </param>
        /// <remarks>
        /// When used properly, it is much faster than similar <see cref="GenTensor{T, IntWrapper}.Iterate" />.
        /// </remarks>
        public void ForEach<TIterator>(TIterator iterator) where TIterator : struct, IValueAction<int[], T>
        {
            static void ForEach1D(GenTensor<T> t, TIterator iterator)
            {
                var index = new int[1];
                for (int i = 0; i < t.Shape[0]; i++)
                {
                    index[0] = i;
                    iterator.Invoke(index, t.GetValueNoCheck(i));
                }
            }
            static void ForEach2D(GenTensor<T> t, TIterator iterator)
            {
                var index = new int[2];
                for (int x = 0; x < t.Shape[0]; x++)
                {
                    index[0] = x;
                    for (int y = 0; y < t.Shape[1]; y++)
                    {
                        index[1] = y;
                        iterator.Invoke(index, t.GetValueNoCheck(index));
                    }
                }
            }
            static void ForEach3D(GenTensor<T> t, TIterator iterator)
            {
                var index = new int[3];
                for (int x = 0; x < t.Shape[0]; x++)
                {
                    index[0] = x;
                    for (int y = 0; y < t.Shape[1]; y++)
                    {
                        index[1] = y;
                        for (int z = 0; z < t.Shape[2]; z++)
                        {
                            index[2] = z;
                            iterator.Invoke(index, t.GetValueNoCheck(index));
                        }
                    }
                }
            }
            static void ForEach4D(GenTensor<T> t, TIterator iterator)
            {
                var index = new int[4];
                for (int x = 0; x < t.Shape[0]; x++)
                {
                    index[0] = x;
                    for (int y = 0; y < t.Shape[1]; y++)
                    {
                        index[1] = y;
                        for (int z = 0; z < t.Shape[2]; z++)
                        {
                            index[2] = z;
                            for (int w = 0; w < t.Shape[3]; w++)
                            {
                                index[3] = w;
                                iterator.Invoke(index, t.GetValueNoCheck(index));
                            }
                        }
                    }
                }
            }
            static bool Next(int[] index, int[] shape)
            {
                index[0]++;
                var i = 0;
                while (index[i] == shape[i])
                {
                    index[i] = 0;
                    if (i == shape.Length - 1)
                        return false;
                    i++;
                    index[i]++;
                }
                return true;
            }

            if (Shape.DimensionCount == 0 || Volume == 0) return;
            if (Shape.DimensionCount == 1) ForEach1D(this, iterator);
            else if (Shape.DimensionCount == 2) ForEach2D(this, iterator);
            else if (Shape.DimensionCount == 3) ForEach3D(this, iterator);
            else if (Shape.DimensionCount == 4) ForEach4D(this, iterator);
            else
            {
                var index = new int[Shape.DimensionCount];
                index[0] = -1;
                while (Next(index, Shape.shape))
                    iterator.Invoke(index, GetValueNoCheck(index));
            }
        }

        /// <summary>
        /// Iterate over array of indices and a value in TPrimitive
        /// </summary>
        public IEnumerable<(int[] Index, T Value)> Iterate()
        {
            foreach (var ind in IterateOver(0))
                yield return (ind, this.GetValueNoCheck(ind));
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z, params int[] indices]
        {
            get => data[GetFlattenedIndexWithCheck(x, y, z, indices)];
            set => data[GetFlattenedIndexWithCheck(x, y, z, indices)] = value;
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z]
        {
            get => data[GetFlattenedIndexWithCheck(x, y, z)];
            set => data[GetFlattenedIndexWithCheck(x, y, z)] = value;
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y]
        {
            get => data[GetFlattenedIndexWithCheck(x, y)];
            set => data[GetFlattenedIndexWithCheck(x, y)] = value;
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x]
        {
            get => data[GetFlattenedIndexWithCheck(x)];
            set => data[GetFlattenedIndexWithCheck(x)] = value;
        }

        /// <summary>
        /// Gets the value by an array of indices.
        /// </summary>
        public T this[int[] inds]
        {
            get => data[GetFlattenedIndexWithCheck(inds)];
            set => data[GetFlattenedIndexWithCheck(inds)] = value;
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x)
        {
            unchecked
            {
                return data[GetFlattenedIndexSilent(x)];
            }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y)
        {
            unchecked
            {
                return data[GetFlattenedIndexSilent(x, y)];
            }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z)
        {
            unchecked
            {
                return data[GetFlattenedIndexSilent(x, y, z)];
            }
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z, int[] indices)
            => data[GetFlattenedIndexSilent(x, y, z, indices)];

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int[] indices)
            => data[GetFlattenedIndexSilent(indices)];

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x)
            => data[GetFlattenedIndexSilent(x)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y)
            => data[GetFlattenedIndexSilent(x, y)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z)
            => data[GetFlattenedIndexSilent(x, y, z)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z, int[] other)
            => data[GetFlattenedIndexSilent(x, y, z, other)] = value;

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int[] indices)
            => data[GetFlattenedIndexSilent(indices)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x)
            => data[GetFlattenedIndexSilent(x)] = valueCreator();

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y)
            => data[GetFlattenedIndexSilent(x, y)] = valueCreator();

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z)
            => data[GetFlattenedIndexSilent(x, y, z)] = valueCreator();

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z, int[] indices)
            => data[GetFlattenedIndexSilent(x, y, z, indices)] = valueCreator();

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int[] indices)
            => data[GetFlattenedIndexSilent(indices)] = valueCreator();

        /// <summary>
        /// If you need to set your wrapper to the tensor directly, use this function
        /// </summary>
        public void SetCell(T newWrapper, params int[] indices)
        {
            var actualIndex = GetFlattenedIndexWithCheck(indices);
            data[actualIndex] = newWrapper;
        }

        /// <summary>
        /// Get a pointer to the wrapper in your tensor
        /// You can call its methods or set its fields, so that it will be applied to the tensor's element
        /// </summary>
        public T GetCell(params int[] indices)
        {
            var actualIndex = GetFlattenedIndexWithCheck(indices);
            return data[actualIndex];
        }

        /// <summary>
        /// Allows to iterate on lower-dimensions,
        /// so that, for example, in tensor of [2 x 3 x 4]
        /// and offsetFromLeft = 1
        /// while iterating you will get the following arrays:
        /// {0, 0}
        /// {0, 1}
        /// {0, 2}
        /// {1, 0}
        /// {1, 1}
        /// {1, 2}
        /// </summary>
        /// <param name="offsetFromLeft"></param>
        /// <returns></returns>
        public IEnumerable<int[]> IterateOver(int offsetFromLeft)
        {
            static bool SumIsNot0(int[] arr)
            {
                foreach (var a in arr)
                    if (a != 0)
                        return true;
                return false;
            }

            if (Volume == 0) yield break;
            var indices = new int[Shape.Count - offsetFromLeft];
            do
            {
                yield return indices;
                NextIndex(indices, indices.Length - 1);
            } while (SumIsNot0(indices)); // for tensor 4 x 3 x 2 the first violating index would be 5  0  0 
        }

        /// <summary>
        /// Allows to iterate on lower-dimensions,
        /// so that, for example, in tensor of [2 x 3 x 4]
        /// and offsetFromLeft = 1
        /// while iterating you will get the following arrays:
        /// {0, 0}
        /// {0, 1}
        /// {0, 2}
        /// {1, 0}
        /// {1, 1}
        /// {1, 2}
        /// </summary>
        /// <param name="offsetFromLeft"></param>
        /// <returns></returns>
        public IEnumerable<int[]> IterateOverCopy(int offsetFromLeft)
        {
            foreach (var inds in IterateOver(offsetFromLeft))
                yield return inds.ToArray();
        }

        /// <summary>
        /// IterateOver where yourTensor[index] is always a matrix
        /// </summary>
        public IEnumerable<int[]> IterateOverMatrices()
            => IterateOver(2);

        /// <summary>
        /// IterateOver where yourTensor[index] is always a vector
        /// </summary>
        public IEnumerable<int[]> IterateOverVectors()
            => IterateOver(1);

        /// <summary>
        /// IterateOver where yourTensor[index] is always an element
        /// </summary>
        public IEnumerable<int[]> IterateOverElements()
            => IterateOver(0);

        public void IterateOver1(Action<int> react)
        {
            for (int x = 0; x < Shape[0]; x++)
                react(x);
        }

        public void IterateOver2(Action<int, int> react)
        {
            for (int x = 0; x < Shape[0]; x++)
            for (int y = 0; y < Shape[1]; y++)
                react(x, y);
        }

        public void IterateOver3(Action<int, int, int> react)
        {
            for (int x = 0; x < Shape[0]; x++)
            for (int y = 0; y < Shape[1]; y++)
            for (int z = 0; z < Shape[2]; z++)
                react(x, y, z);
        }
    }
}
