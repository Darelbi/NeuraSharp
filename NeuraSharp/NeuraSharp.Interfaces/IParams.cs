using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface IParams<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        void AddArrayParameter(string name, T[] value);
        void AddParameter(string name, T value);

        void AddIntParameter(string name, int value);

        void AddIntArrayParameter(string name, int[] value);

        T[] GetArrayParameter(string name);
        T GetParameters(string name);

        int GetIntParameter(string name);

        int[] GetIntArrayParameter(string name);
    }
}
