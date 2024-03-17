namespace NeuraSharp.Enums
{
    /// <summary>
    /// List of built-in activation functions
    /// </summary>
    public enum ActivationFunction
    {
        /// <summary>
        /// Provides a smooth step which saturates between 0 and 1, common choice in output layer
        /// if output must be between 0 and 1
        /// </summary>
        Sigmoid,

        /// <summary>
        /// Provides a smooth step which saturates between -1 and 1, common choice in output layer
        /// if output must be between -1 and 1
        /// </summary>
        Tanh,

        /// <summary>
        /// Relay the input, but only if greater than 0, common choice in Deep neural networks
        /// to avoid saturation
        /// </summary>
        ReLU,

        /// <summary>
        /// Like relu, but a small percentage of input is relayed even if negative
        /// </summary>
        LeakyReLU,

        /// <summary>
        /// Experimental function, do not use
        /// </summary>
        Olly
    }
}
