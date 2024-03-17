using NeuraSharp.BuiltIn;
using System.Numerics;
using System.Reflection;

namespace NeuraSharp
{
    public static class InstanceLoader
    {
        /// <summary>
        /// Just create a neura builder with built-in functionality
        /// </summary>
        public static NeuraBuilder<float> BuilderFromBuiltIn()
        {
            return BuilderFromAssemblies<float>( [GetBuiltInAssembly()] );
        }

        /// <summary>
        /// Make it available in case someone wants to load it besides other stuff
        /// </summary>
        /// <returns></returns>
        public static Assembly GetBuiltInAssembly()
        {
            // make sure the entry point is the same also for built-in library even though we could 
            // initialize in other ways. keep it DRY (don't repeat yourself)
            return Assembly.GetAssembly(typeof(ReferenceMe))!;
        }

        /// <summary>
        /// Creates a Neural network builder. The entry point for your application. You don't need to locate
        /// DLL or stuff. Just reference a type from your library, and call Assembly.GetAssembly(yourtype)
        /// on it. Like I did :)
        /// </summary>
        /// <typeparam name="T">Allows to select between float or double. For some rare applications 
        /// "double" is just better: https://arxiv.org/abs/2209.07219 And anyway we leverage INumber interface
        /// allowing also for other number types eventually</typeparam>
        /// <param name="assembliesToLoad">Assemblies where to search for implemented interfaces</param>
        /// <returns></returns>
        public static NeuraBuilder<T> BuilderFromAssemblies<T>(params Assembly[] assembliesToLoad) where T: INumber<T>, IFloatingPointIeee754<T>
        {
            return new NeuraBuilder<T>();
        }
    }
}
