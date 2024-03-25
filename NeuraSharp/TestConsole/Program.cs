// See https://aka.ms/new-console-template for more information


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
// with adam to se if the network works.

int neurons1 = 784;
int neurons2 = 32;
int neurons3 = 10;

var wInit = new HeUniformInitialization<float>();
var bInit = new ZeroBiasInitializer<float>();

var layer0 = new BaseNeuralLayer<float>(0, 0, 3, wInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());
var layer1 = new BaseNeuralLayer<float>(1, 3, neurons1, wInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());
var layer2 = new BaseNeuralLayer<float>(2, neurons1, neurons2, wInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());
var layer3 = new BaseNeuralLayer<float>(3, neurons2, neurons3, wInit, bInit,
    new ReLUActivationFunction<float>(), new NoneRegularization<float>());

var huberParams = new Params<float>(false);
var boundedAdamParams = new Params<float>(false);
huberParams.AddParameter(Params.Delta, 0.79f);

boundedAdamParams.AddParameter(Params.Beta1, 0.9f);
boundedAdamParams.AddParameter(Params.Beta2, 0.999f);
boundedAdamParams.AddParameter(Params.Epsilon, 1e-8f);
boundedAdamParams.AddParameter(Params.Min, 0.001f);
boundedAdamParams.AddParameter(Params.Max, 0.5f);


var network =
new NeuraNetwork<float>(
    [layer0, layer1, layer2, layer3],
    new DefaultForwardAlgorithm<float>(),
    new DefaultBackwardAlgorithm<float>(new PseudoHuberLossFunction<float>(huberParams)),
    new ExpBoundedAdamOptimizer<float>(boundedAdamParams), //TODO NETWORK RUNINT SOURCE TO BE CONIFUGRED
    new LayerAllocatedVariables<float>(4));

//for (int i = 0; i < 100; i++)
//    network.Fit(myEnum, 0.01f, 100);