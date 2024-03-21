using System.Numerics;

namespace NeuraSharp.Interfaces.Layers
{
    public interface ITotalGradientsLayer<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public T[] TotalGradients { get; set; }
    }
}
