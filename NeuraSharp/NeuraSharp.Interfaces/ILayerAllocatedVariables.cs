/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
using System.Numerics;

namespace NeuraSharp.Interfaces
{
    public interface ILayerAllocatedVariables<T> where T : INumber<T>, IFloatingPointIeee754<T>
    {
        void AddArrayVariable(string name, T[] value);
        void AddArrayVariable(Enums.Params param, T[] value);
        void AddVariable(string name, T value);
        void AddVariable(Enums.Params param, T value);
        void AddIntVariable(string name, int value);
        void AddIntVariable(Enums.Params param, int value);

        void UpdateArrayVariable(string name, T[] value);
        void UpdateArrayVariable(Enums.Params param, T[] value);
        void UpdateVariable(string name, T value);
        void UpdateVariable(Enums.Params param, T value);
        void UpdateIntVariable(string name, int value);
        void UpdateIntVariable(Enums.Params param, int value);

        T[] GetArrayVariable(string name);
        T[] GetArrayVariable(Enums.Params param);
        T GetVariable(string name);
        T GetVariable(Enums.Params param);
        int GetIntVariable(string name);
        int GetIntVariable(Enums.Params param);
    }
}
