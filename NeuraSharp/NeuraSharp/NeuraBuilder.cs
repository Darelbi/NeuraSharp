using NeuraSharp.BuiltIn;
using NeuraSharp.Interfaces;
using System.Numerics;

namespace NeuraSharp
{
    public class NeuraBuilder<T> : INeuraBuilder<T> where T:INumber<T>, IFloatingPointIeee754<T>
    {
        private List<INeuralLayer<T>> layers = [];
        private IForwardAlgorithm<T> algorithm = null;

        public INeuraNetwork<T> Compile()
        {
            if (algorithm == null)
                throw new System.ArgumentNullException("No optimization algorithm selected");

            if (layers.Count < 2)
                throw new System.ArgumentException("At least two layers (input,output) required");

            if (layers.Any(x => x == null))
                throw new System.ArgumentException("Some layers were null");

            //return new NeuraNetwork<T>([.. layers], algorithm);
            return null;
        }
    }
}
