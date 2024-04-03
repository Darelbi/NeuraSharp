/*
    Author: Dario Oliveri ( https://github.com/Darelbi )
    Copyright (c) 2024 Dario Oliveri
    License: MIT (see LICENSE file in repository root for more detail)
*/
namespace NeuraSharp.Interfaces
{
    public interface IStepSource
    {
        public int GetStep();

        public void IncreaseSteps();
    }
}
