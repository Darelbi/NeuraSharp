using System.Numerics;
using NeuraSharp.Interfaces;

namespace NeuraSharp.Logic
{
    public class Params<T>: IParams<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly Dictionary<string, T> Parameters = [];
        private readonly Dictionary<string, T[]> ArrayParameters = [];
        private readonly Dictionary<string, int> IntParameters = [];
        private readonly Dictionary<string, int[]> IntArrayParameters = [];

        public Params() { }

        public void AddParameter(string name, T value)
        {
            Parameters.Add(name, value);
        }

        public void AddArrayParameter(string name, T[] value)
        {
            ArrayParameters.Add(name, value);
        }

        public T[] GetArrayParameter(string name)
        {
             return ArrayParameters[name];
        }

        public T GetParameters(string name)
        {
            return Parameters[name];
        }

        public int GetIntParameter(string name)
        {
            return IntParameters[name];
        }

        public int[] GetIntArrayParameter(string name)
        {
            return IntArrayParameters[name];
        }

        public void AddIntParameter(string name, int value)
        {
            IntParameters[name] = value;
        }

        public void AddIntArrayParameter(string name, int[] value)
        {
            IntArrayParameters[name] = value;
        }
    }
}
