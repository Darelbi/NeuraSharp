using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface INeuraBuilder<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        INeuraNetwork<T> Compile();
    }
}
