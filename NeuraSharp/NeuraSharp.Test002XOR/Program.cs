using NeuraSharp.BuiltIn;
using NeuraSharp.BuiltIn.ActivationFunction;
using NeuraSharp.BuiltIn.BackwardAlgorithm;
using NeuraSharp.BuiltIn.BiasInitialization;
using NeuraSharp.BuiltIn.ForwardAlgorithm;
using NeuraSharp.BuiltIn.Layers;
using NeuraSharp.BuiltIn.LossFunction;
using NeuraSharp.BuiltIn.Optimizers;
using NeuraSharp.BuiltIn.Regularization;
using NeuraSharp.BuiltIn.WeightInitialization;
using NeuraSharp.Interfaces.Enums;
using NeuraSharp.Logic;

// before wasting time on building the network builder I do a MNIST task with Gradient Descent and a MNIST task
// with adam to see if the network works.

int neurons1 = 2;
int neurons2 = 6;
int neurons3 = 6;
int neurons4 = 1;

var heInit = new HeUniformInitialization<float>();
var glInit = new GlorotUniformInitialization<float>();
var bInit = new ZeroBiasInitializer<float>();

var layer0 = new BaseNeuralLayer<float>(0, 0, neurons1, heInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());
var layer1 = new BaseNeuralLayer<float>(1, neurons1, neurons2, heInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());
var layer2 = new BaseNeuralLayer<float>(2, neurons2, neurons3, heInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());
var layer3 = new BaseNeuralLayer<float>(3, neurons3, neurons4, glInit, bInit,
    new SigmoidActivationFunction<float>(), new NoneRegularization<float>());

var huberParams = new Params<float>(false);
huberParams.AddParameter(Params.Delta, 0.1f);

var network =
new NeuraNetwork<float>(
    [layer0, layer1, layer2, layer3],
    new DefaultForwardAlgorithm<float>(),
    new DefaultBackwardAlgorithm<float>(new MeanSquareFunction<float>()),
    new GradientDescentOptimizer<float>(),
    new LayerAllocatedVariables<float>(3));

float zero = -0.5f;
float one = 0.5f;

var batch = new List<(float[] inputs, float[] outputs)>
{
    ( [one,zero], [one]),
    ( [zero,one], [one]),
    ( [zero,zero], [zero]),
    ( [one,one], [zero]),
};

var a  = new List<(float[] inputs, float[] outputs)> { ([one, zero], [one]) };
var b = new List<(float[] inputs, float[] outputs)>{    ([zero, one], [one]) };
var c = new List<(float[] inputs, float[] outputs)>{    ([zero, zero], [zero]) };
var d = new List<(float[] inputs, float[] outputs)>{    ([one, one], [zero]) };

int epochs = 5000;

using (StreamWriter writer = new StreamWriter("Error.csv"))
{
    for (int i = 0; i < epochs; i++)
    {
        network.Fit([a], 0.02f, epochs, x =>
        {
            //Console.WriteLine($"Error: {x}");
            writer.WriteLine(x);
        });
        network.Fit([b], 0.02f, epochs, x =>
        {
            //Console.WriteLine($"Error: {x}");
            writer.WriteLine(x);
        });
        network.Fit([c], 0.02f, epochs, x =>
        {
            //Console.WriteLine($"Error: {x}");
            writer.WriteLine(x);
        });
        network.Fit([d], 0.02f, epochs, x =>
        {
            //Console.WriteLine($"Error: {x}");
            writer.WriteLine(x);
        });
    }
}

network.Compile();

Console.WriteLine("Testing...");


foreach (var sample in batch)
{
    var result = network.Predict(sample.inputs);
    Console.WriteLine($"expected {sample.outputs[0]}, obtained: {result[0]}");
}

//for (int i = 0; i < 100; i++)
//    network.Fit(myEnum, 0.01f, 100);