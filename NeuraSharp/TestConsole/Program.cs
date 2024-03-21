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
using NeuraSharp.Interfaces.Enums;

InstanceLoader.BuilderFromBuiltIn();

List < List < (float[] inputs, float[] outputs) > > myEnum = new();

myEnum.Add(new List<(float[] inputs, float[] outputs)> { (new float[] { 1, 2, 3 }, new float[] { 2, 3 }) });
myEnum.Add(new List<(float[] inputs, float[] outputs)> { (new float[] { 2, 3, 4 }, new float[] { 0, -1 }) });
myEnum.Add(new List<(float[] inputs, float[] outputs)> { (new float[] { 5, 6, 7 }, new float[] { -2, 3 }) });

var layer0 = new BaseNeuralLayer<float>(new ReLUActivationFunction<float>());
var layer1 = new BaseNeuralLayer<float>(new ReLUActivationFunction<float>());
var layer2 = new BaseNeuralLayer<float>(new ReLUActivationFunction<float>());
var layer3 = new BaseNeuralLayer<float>(new ReLUActivationFunction<float>());

int neurons1 = 8;
int neurons2 = 7;
int neurons3 = 2;

layer0.Initialize(0, 0, 3);
layer1.Initialize(1, 3, neurons1);
layer2.Initialize(2, neurons1, neurons2);
layer3.Initialize(3, neurons2, neurons3);

var networkParams = new NeuraSharp.Logic.Params<float>();
var huberParams = new NeuraSharp.Logic.Params<float>();
var adamParams = new NeuraSharp.Logic.Params<float>();
huberParams.AddParameter(Params.Delta, 0.79f);
networkParams.AddParameter(Params.LearningRate, 0.001f);
adamParams.AddParameter(Params.Beta1,0.9f);
adamParams.AddParameter(Params.Beta2,0.999f);
adamParams.AddParameter(Params.Epsilon, 1e-8f);

var network =
new NeuraNetwork<float>(
    [layer0, layer1, layer2, layer3],
    new DefaultForwardAlgorithm<float>(new StableNeuronSummation<float>()),
    new DefaultBackwardAlgorithm<float>(new PseudoHuberLossFunction<float>(huberParams)),
    new AdamOptimizer<float>(adamParams),
    null,
    networkParams);

network.Fit(myEnum);