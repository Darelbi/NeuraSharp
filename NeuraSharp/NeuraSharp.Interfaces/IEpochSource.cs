namespace NeuraSharp.Interfaces
{
    public interface IEpochSource
    {
        int GetEpoch();
        int GetTotalEpochs();
        void IncreaseEpoch();
    }
}
