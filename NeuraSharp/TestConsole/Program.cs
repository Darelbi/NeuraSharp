// See https://aka.ms/new-console-template for more information


using NeuraSharp;
using NeuraSharp.BuiltIn;
using NeuraSharp.BuiltIn.ActivationFunction;
using NeuraSharp.BuiltIn.BackwardAlgorithm;
using NeuraSharp.BuiltIn.ForwardAlgorithm;
using NeuraSharp.BuiltIn.Layers;
using NeuraSharp.BuiltIn.LossFunction;
using NeuraSharp.BuiltIn.NeuronSummation;
using NeuraSharp.BuiltIn.Optimizers;
using NeuraSharp.BuiltIn.Regularization;
using NeuraSharp.Interfaces.Enums;
using NeuraSharp.Logic;

/// THIS FILE IS FOR TESTING DURING DEVELOPMENT. IT LOOKS VERBOSE BECAUSE THERE IS YET NOT BUILDER
/// THE BUILDER WILL ALLOW A MUCH MORE CLEAN INITIALIZATION
InstanceLoader.DefaultBuilder();

List<List<(float[] inputs, float[] outputs)>> myEnum = new();

myEnum.Add(new List<(float[] inputs, float[] outputs)> { (new float[] { 1, 2, 3 }, new float[] { 2, 3 }) });
myEnum.Add(new List<(float[] inputs, float[] outputs)> { (new float[] { 2, 3, 4 }, new float[] { 0, -1 }) });
myEnum.Add(new List<(float[] inputs, float[] outputs)> { (new float[] { 5, 6, 7 }, new float[] { -2, 3 }) });

var regulParams = new Params<float>(false);
regulParams.AddParameter(Params.DropoutChance, 0.3f);

int neurons1 = 8;
int neurons2 = 7;
int neurons3 = 2;

var layer0 = new BaseNeuralLayer<float>(0, 0, 3,
    new ReLUActivationFunction<float>(), new PseudoDropOutRegularization<float>(regulParams));
var layer1 = new BaseNeuralLayer<float>(1, 3, neurons1,
    new ReLUActivationFunction<float>(), new PseudoDropOutRegularization<float>(regulParams));
var layer2 = new BaseNeuralLayer<float>(2, neurons1, neurons2,
    new ReLUActivationFunction<float>(), new PseudoDropOutRegularization<float>(regulParams));
var layer3 = new BaseNeuralLayer<float>(3, neurons2, neurons3,
    new ReLUActivationFunction<float>(), new PseudoDropOutRegularization<float>(regulParams));


var huberParams = new Params<float>(false);
var boundedAdamParams = new Params<float>(false);
huberParams.AddParameter(Params.Delta, 0.79f);

boundedAdamParams.AddParameter(Params.Beta1, 0.9f);
boundedAdamParams.AddParameter(Params.Beta2, 0.999f);
boundedAdamParams.AddParameter(Params.Epsilon, 1e-8f);
boundedAdamParams.AddParameter(Params.MinBound, 0.001f);
boundedAdamParams.AddParameter(Params.MaxBound, 0.5f);


var network =
new NeuraNetwork<float>(
    [layer0, layer1, layer2, layer3],
    new DefaultForwardAlgorithm<float>(new StableNeuronSummation<float>()),
    new DefaultBackwardAlgorithm<float>(new PseudoHuberLossFunction<float>(huberParams)),
    new ExpBoundedAdamOptimizer<float>(boundedAdamParams), //TODO NETWORK RUNINT SOURCE TO BE CONIFUGRED
    new LayerAllocatedVariables<float>(4));

for (int i = 0; i < 100; i++)
    network.Fit(myEnum, 0.01f, 100);