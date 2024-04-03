using GenericTensor.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeuraSharp.UnitTest.GenericTensor.TestUtils
{
    public static class TensorEquals
    {
        public static void AssertTensorEquals(GenTensor<float> a, GenTensor<float> b)
        {
            for (int i = 0; i < a.data.Length; i++)
                Assert.AreEqual(a.data[i], b.data[i], 0.000001f);
        }

        public static void AssertTensorEquals(GenTensor<double> a, GenTensor<double> b)
        {
            for (int i = 0; i < a.data.Length; i++)
                Assert.AreEqual(a.data[i], b.data[i], 0.000001);
        }

        public static void AssertTensorEquals(GenTensor<decimal> a, GenTensor<decimal> b)
        {
            for (int i = 0; i < a.data.Length; i++)
                Assert.AreEqual(a.data[i], b.data[i], 0.000001m);
        }
    }
}
