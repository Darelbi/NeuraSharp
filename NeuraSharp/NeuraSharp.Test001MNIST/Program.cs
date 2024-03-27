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

int neurons1 = 784;
int neurons2 = 64;
int neurons3 = 64;
int neurons4 = 49;
int neurons5 = 10;

var wInit = new GlorotUniformInitialization<float>();
var heInit = new HeUniformInitialization<float>();
var bInit = new ZeroBiasInitializer<float>();

var layer0 = new BaseNeuralLayer<float>(0, 0, neurons1, wInit, bInit,
    new TanhActivationFunction<float>(), new NoneRegularization<float>());
var layer1 = new BaseNeuralLayer<float>(1, neurons1, neurons2, wInit, bInit,
    new TanhActivationFunction<float>(), new NoneRegularization<float>());
var layer2 = new BaseNeuralLayer<float>(2, neurons2, neurons3, wInit, bInit,
    new TanhActivationFunction<float>(), new NoneRegularization<float>());
var layer3 = new BaseNeuralLayer<float>(2, neurons3, neurons4, wInit, bInit,
    new TanhActivationFunction<float>(), new NoneRegularization<float>());
var layer4 = new BaseNeuralLayer<float>(2, neurons4, neurons5, wInit, bInit,
    new TanhActivationFunction<float>(), new NoneRegularization<float>());

var huberParams = new Params<float>(false);
huberParams.AddParameter(Params.Delta, 0.05f);

var network =
new NeuraNetwork<float>(
    [layer0, layer1, layer2, layer3, layer4],
    new DefaultForwardAlgorithm<float>(),
    new DefaultBackwardAlgorithm<float>(new MeanSquareFunction<float>()),
    new GradientDescentOptimizer<float>(),
    new LayerAllocatedVariables<float>(3));


var lines = File.ReadLines(@"..\..\..\..\..\datasets\MNIST\mnist_train.csv");
var dic = new Dictionary<int, (float[] input, float[] outpus)[]>();
var dicList = new Dictionary<int, List<(float[] input, float[] outpus)>>();
foreach (var line in lines)
{
    var csvline = line.Split(',');
    var outdigit = csvline.Take(1).Select(x => int.Parse(x)).First();

    var output = new float[10];
    for (int k = 0; k < 10; k++)
        output[k] = -1;
    output[outdigit] = 1.0f;

    var image = csvline.Skip(1).Take(784).Select(x => (int.Parse(x) - 128) / 255.0f).ToArray();

    if (!dicList.ContainsKey(outdigit))
        dicList[outdigit] = new List<(float[] input, float[] outpus)>();

    dicList[outdigit].Add((image, output));
}

for (int i = 0; i < 10; i++)
    dic[i] = dicList[i].ToArray();

int epochs = 200000;
int bbb = 0;

using (StreamWriter writer = new StreamWriter("TestingLR00050_5layers.csv"))
{
    for (int i = 0; i < epochs; i++)
    {
        Console.WriteLine("Traning epoch: " + i);

        var batch = new List<(float[] inputs, float[] outputs)>();

        int digit = Random.Shared.Next(0, 10);

        int max = dic[digit].Length;

        for (int k = 0; k < 8; k++)
        {
            batch.Add(dic[digit][ Random.Shared.Next(0, max)]);
        }

        network.Fit([batch], 0.00050f, epochs, x =>
        {
            //bbb++;
            //if (bbb > 450)
            //{
            //    writer.WriteLine(x);
            //    bbb = 0;
            //}
        });
        batch.Clear();

        if(i%300==0)
        {
            // BEGIN TEST WITH TEST

            var lines2 = File.ReadLines(@"..\..\..\..\..\datasets\MNIST\mnist_test.csv").ToArray();

            Console.WriteLine("Testing...");

            int total = 0;
            int wrong = 0;
            //foreach (var line in lines2)
            for(int z=0; z<8000; z++)
            {
                var line = lines2[Random.Shared.Next(0, lines2.Length)];
                total++;
                var csvline = line.Split(',');
                var outdigit = csvline.Take(1).Select(x => int.Parse(x)).First();

                var output = new float[10];
                output[outdigit] = 1.0f;

                var image = csvline.Skip(1).Take(784).Select(x => (int.Parse(x) - 128) / 255.0f).ToArray();

                var digits = network.Predict(image);

                int maxIndex = -1;
                float maxValue = float.MinValue;
                for (int n = 0; n < 10; n++)
                {
                    if (digits[n] > maxValue)
                    {
                        maxValue = digits[n];
                        maxIndex = n;
                    }
                }

                if (outdigit != maxIndex)
                {
                    //Console.WriteLine("Wrong value found");
                    wrong++;
                }
            }
            writer.WriteLine(((total - wrong) / (float)total));
            Console.WriteLine("Accuracy =" + ((total - wrong) / (float)total));
        }// END TEST WITH TEST
    }
}

network.Compile();


//for (int i = 0; i < 100; i++)
//    network.Fit(myEnum, 0.01f, 100);