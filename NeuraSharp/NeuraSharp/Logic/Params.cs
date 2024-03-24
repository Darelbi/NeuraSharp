using System.Numerics;
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;

namespace NeuraSharp.Logic
{
    public class Params<T>: IParams<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly Dictionary<string, T> Parameters = [];
        private readonly Dictionary<string, T[]> ArrayParameters = [];
        private readonly Dictionary<string, int> IntParameters = [];
        private readonly Dictionary<string, int[]> IntArrayParameters = [];
        private readonly Dictionary<string, string> UsedParameters = [];
        private readonly bool allowsUpdate;

        public Params(bool allowsUpdate)
        {
            this.allowsUpdate = allowsUpdate;
        }

        private static string FromParam(Params param) { return param.ToString(); }

        public void CheckUsedParameter(string name) 
        {
            if (UsedParameters.ContainsKey(name) && !allowsUpdate)
                throw new ArgumentException("This parameter name was already used");

            UsedParameters[name] = name;
        }

        public void AddParameter(string name, T value)
        {
            CheckUsedParameter(name);
            Parameters.Add(name, value);
        }

        public void AddArrayParameter(string name, T[] value)
        {
            CheckUsedParameter(name);
            ArrayParameters.Add(name, value);
        }

        public T[] GetArrayParameter(string name)
        {

             return ArrayParameters[name];
        }

        public T GetParameter(string name)
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
            CheckUsedParameter(name);
            IntParameters[name] = value;
        }

        public void AddIntArrayParameter(string name, int[] value)
        {
            CheckUsedParameter(name);
            IntArrayParameters[name] = value;
        }

        public void AddArrayParameter(Params param, T[] value)
        {
            AddArrayParameter(FromParam(param), value);
        }

        public void AddParameter(Params param, T value)
        {
            AddParameter(FromParam(param), value);
        }

        public void AddIntParameter(Params param, int value)
        {
            AddIntParameter(FromParam(param), value);
        }

        public void AddIntArrayParameter(Params param, int[] value)
        {
            AddIntArrayParameter(FromParam(param), value);
        }

        public T[] GetArrayParameter(Params param)
        {
            return GetArrayParameter(FromParam(param));
        }

        public T GetParameter(Params param)
        {
            return GetParameter(FromParam(param));
        }

        public int GetIntParameter(Params param)
        {
            return GetIntParameter(FromParam(param));
        }

        public int[] GetIntArrayParameter(Params param)
        {
            return GetIntArrayParameter(FromParam(param));
        }
    }
}
