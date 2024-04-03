/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
    Original Project: https://github.com/Darelbi/NeuraSharp/ */
namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// All factories implements this, the name allow the builder to find the correct factory
    /// when lookin up strings or enums
    /// </summary>
    public interface IFactory
    {
        public string GetName();
    }
}
