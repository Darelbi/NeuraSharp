namespace NeuraSharp.Enums
{
    /// <summary>
    /// Customize the algorithm that sum the weighted inputs for each neuron
    /// Each neuron output is the scalar product of inputs and weights and 
    /// then activation function is applied.
    /// </summary>
    public enum NeuronSummation
    {
        /// <summary>
        /// Just sum the weighted inputs
        /// </summary>
        Fast,

        /// <summary>
        /// Sort the weighted inputs before summing to reduce error (slower)
        /// </summary>
        Stable,

        /// <summary>
        /// Just take the maximum weighted input
        /// </summary>
        Max
    }
}
