/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using System.Numerics;

namespace NeuraSharp.Logic
{
    public class LayerAllocatedVariables<T> : ILayerAllocatedVariables<T>, ILayerAllocatedConfiguration<T>
         where T : INumber<T>, IFloatingPointIeee754<T>
    {
        private readonly Params<T>[] layers;
        private int currentLayer = 0;

        public LayerAllocatedVariables(int numberOfLayers)
        {
            layers = new Params<T>[numberOfLayers];
            for (int i = 0; i < numberOfLayers; i++)
                layers[i] = new Params<T>(true);
        }

        public void AddArrayVariable(string name, T[] value)
        {
            layers[currentLayer].AddArrayParameter(name, value);
        }

        public void AddArrayVariable(Params param, T[] value)
        {
            layers[currentLayer].AddArrayParameter(param, value);
        }

        public void AddIntVariable(string name, int value)
        {
            layers[currentLayer].AddIntParameter(name, value);
        }

        public void AddIntVariable(Params param, int value)
        {
            layers[currentLayer].AddIntParameter(param, value);
        }

        public void AddVariable(string name, T value)
        {
            layers[currentLayer].AddParameter(name, value); 
        }

        public void AddVariable(Params param, T value)
        {
            layers[currentLayer].AddParameter(param, value);
        }

        public T[] GetArrayVariable(string name)
        {
            return layers[currentLayer].GetArrayParameter(name);
        }

        public T[] GetArrayVariable(Params param)
        {
            return layers[currentLayer].GetArrayParameter(param);
        }

        public int GetIntVariable(string name)
        {
            return layers[currentLayer].GetIntParameter(name);
        }

        public int GetIntVariable(Params param)
        {
            return layers[currentLayer].GetIntParameter(param);
        }

        public T GetVariable(string name)
        {
            return layers[currentLayer].GetParameter(name);
        }

        public T GetVariable(Params param)
        {
            return layers[currentLayer].GetParameter(param);
        }

        public void SetLayer(int layerIndex0based)
        {
            currentLayer=layerIndex0based;
        }

        public void UpdateArrayVariable(string name, T[] value)
        {
            layers[currentLayer].AddArrayParameter(name, value);
        }

        public void UpdateArrayVariable(Params param, T[] value)
        {
            layers[currentLayer].AddArrayParameter(param, value);
        }

        public void UpdateIntVariable(string name, int value)
        {
            layers[currentLayer].AddIntParameter(name, value);
        }

        public void UpdateIntVariable(Params param, int value)
        {
            layers[currentLayer].AddIntParameter(param, value);
        }

        public void UpdateVariable(string name, T value)
        {
            layers[currentLayer].AddParameter(name, value);
        }

        public void UpdateVariable(Params param, T value)
        {
            layers[currentLayer].AddParameter(param, value);
        }
    }
}
