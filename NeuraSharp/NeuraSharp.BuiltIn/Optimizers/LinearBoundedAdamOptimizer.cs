using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class LinearBoundedAdamOptimizer<T> : BoundedAdamOptimizer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public LinearBoundedAdamOptimizer(IParams<T> adamParams, INetworkTuningSource<T> source) : base(adamParams, source)
        {

        }

        public override T GetAscendingBound(T minima, T maxima, T progresss)
        {
            return minima + (maxima - minima) * progresss;
        }

        public override T GetDescendingBound(T minima, T maxima, T progresss)
        {
            return maxima - (maxima - minima) * progresss;
        }
    }
}
