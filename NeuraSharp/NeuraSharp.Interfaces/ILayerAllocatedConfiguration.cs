using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface ILayerAllocatedConfiguration<T> 
        : ILayerAllocatedVariables<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        public void SetLayer(int layerIndex0based);
    }
}
