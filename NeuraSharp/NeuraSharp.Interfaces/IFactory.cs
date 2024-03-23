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
