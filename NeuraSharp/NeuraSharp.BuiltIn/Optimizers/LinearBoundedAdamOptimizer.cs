using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Optimizers
{
    public class LinearBoundedAdamOptimizer<T>(IParams<T> adamParams) : BoundedAdamOptimizer<T>(adamParams) where T : INumber<T>, IFloatingPointIeee754<T>
    {
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
