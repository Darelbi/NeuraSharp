using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class TrigonometricBoundedAdamOptimizer<T> : BoundedAdamOptimizer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private T two;

        public TrigonometricBoundedAdamOptimizer(IParams<T> adamParams, IRunningMetadata<T> source) : base(adamParams, source)
        {
            two = T.One + T.One;
        }

        public override T GetAscendingBound(T minima, T maxima, T progresss)
        {
            return minima + (maxima - minima)*T.Sin(progresss * T.Pi / two);
        }

        public override T GetDescendingBound(T minima, T maxima, T progresss)
        {
            return maxima - (maxima - minima) * T.Sin(progresss * T.Pi / two);
        }
    }
}
