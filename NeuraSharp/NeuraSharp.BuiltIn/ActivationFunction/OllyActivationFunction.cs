using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp.BuiltIn.ActivationFunction
{
    /// <summary>
    /// Do not use this, I created it and I'm playing with it but beware there is no research going on with
    /// this function. I think it should avoid some problems providing some of the advantages of other
    /// functions. And it work well in SMALL tests, but no big test done so far!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OllyActivationFunction<T> : IActivationFunction<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T Compute(T weightedSum)
        {
            if (weightedSum < T.Zero)
                return (T.Exp(weightedSum) - T.One);

            return T.Log(weightedSum + T.One);
        }

        public T Derivate(T weightedSum)
        {
            if (weightedSum < T.Zero)
                return T.Exp(weightedSum);

            return T.One / (weightedSum + T.One);
        }
    }
}
