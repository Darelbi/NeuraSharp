using System.Numerics;

namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// Initialization static params. Those values are meant to be copied in the constructor
    /// and a reference to IParams should not be retained. These params allow the builder to
    /// setup values which are getted by implementers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParams<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        void AddArrayParameter(string name, T[] value);
        void AddArrayParameter(Enums.Params param, T[] value);
        void AddParameter(string name, T value);
        void AddParameter(Enums.Params param, T value);
        void AddIntParameter(string name, int value);
        void AddIntParameter(Enums.Params param, int value);
        void AddIntArrayParameter(string name, int[] value);
        void AddIntArrayParameter(Enums.Params param, int[] value);
        T[] GetArrayParameter(string name);
        T[] GetArrayParameter(Enums.Params param);
        T GetParameter(string name);
        T GetParameter(Enums.Params param);
        int GetIntParameter(string name);
        int GetIntParameter(Enums.Params param);
        int[] GetIntArrayParameter(string name);
        int[] GetIntArrayParameter(Enums.Params param);
    }
}
