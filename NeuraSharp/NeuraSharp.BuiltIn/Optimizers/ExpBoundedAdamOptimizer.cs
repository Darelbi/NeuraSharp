using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class ExpBoundedAdamOptimizer<T> : BoundedAdamOptimizer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public ExpBoundedAdamOptimizer(IParams<T> adamParams) : base(adamParams)
        {

        }

        public override T GetAscendingBound(T minima, T maxima, T progresss)
        {
            return minima * T.Pow(maxima / minima, progresss);
        }

        public override T GetDescendingBound(T minima, T maxima, T progresss)
        {
            return maxima * T.Pow(minima / maxima, progresss);
        }
    }
}
