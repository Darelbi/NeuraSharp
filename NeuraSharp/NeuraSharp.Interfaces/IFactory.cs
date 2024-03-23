using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuraSharp.Interfaces
{
    /// <summary>
    /// All factories implements this, the name allow the builder to find the correct factory
    /// </summary>
    public interface IFactory
    {
        public string GetName();
    }
}
