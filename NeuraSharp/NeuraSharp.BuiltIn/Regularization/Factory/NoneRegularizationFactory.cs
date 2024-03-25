using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Factories;
using System.Numerics;

namespace NeuraSharp.BuiltIn.Regularization.Factory
{
    public class NoneRegularizationFactory<T> : IRegularizationAglorithmFactory<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public IRegularizationAlgorithm<T> Create(IParams<T> regulParams)
        {
            return new NoneRegularization<T>();
        }

        public string GetName()
        {
            return "None";
        }
    }
}
