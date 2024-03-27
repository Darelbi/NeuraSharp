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
using NeuraSharp.Interfaces;
using NeuraSharp.Interfaces.Enums;
using NeuraSharp.Logic;

using (StreamWriter writer = new StreamWriter("Compare random nets.csv"))
{
    for (int mmm = 0; mmm < 50; mmm++)
    {
        string layerDescription = "";
        var numLayers = Random.Shared.Next(1, 4);
        layerDescription += $"\"{numLayers}:[784-";

        List<int> numNeurons = new List<int>();
        numNeurons.Add(0);
        numNeurons.Add(784);

        int[] numNeuronsLookup = [ 16, 25, 36, 49, 64, 81, 100, 121];

        for (int i = 0; i < numLayers; i++)
        {
            var num = numNeuronsLookup[Random.Shared.Next(0, 6)];
            numNeurons.Add(num);
            layerDescription += $"{num}-";
        }

        numNeurons.Add(10);
        layerDescription += $"10]x";

        var glorot = new GlorotUniformInitialization<float>();
        var he = new HeUniformInitialization<float>();
        var bias = new ZeroBiasInitializer<float>();

        List<INeuralLayer<float>> layers = new();
        for (int i = 0; i < numNeurons.Count - 1; i++)
        {
            INeuralLayer<float> layer = null;
            int layerChoice = Random.Shared.Next(0, 5);
            switch (
                layerChoice)
            {
                case 0:
                    layerDescription += "(tanh)x";
                    layer = new BaseNeuralLayer<float>(0, numNeurons[i], numNeurons[i + 1], glorot, bias,
                        new TanhActivationFunction<float>(), new NoneRegularization<float>()); break;
                case 1:
                    layerDescription += "(sigm)x";
                    layer = new BaseNeuralLayer<float>(0, numNeurons[i], numNeurons[i + 1], glorot, bias,
                        new SigmoidActivationFunction<float>(), new NoneRegularization<float>()); break;
                case 2:
                    layerDescription += "(relu)x";
                    layer = new BaseNeuralLayer<float>(0, numNeurons[i], numNeurons[i + 1], he, bias,
                        new ReLUActivationFunction<float>(), new NoneRegularization<float>()); break;
                case 3:
                    layerDescription += "(leaky)x";
                    layer = new BaseNeuralLayer<float>(0, numNeurons[i], numNeurons[i + 1], he, bias,
                        new LeakyReLUActivationFunction<float>(), new NoneRegularization<float>()); break;
                case 4:
                    layerDescription += "(olly)x";
                    layer = new BaseNeuralLayer<float>(0, numNeurons[i], numNeurons[i + 1], glorot, bias,
                        new OllyActivationFunction<float>(), new NoneRegularization<float>()); break;
            }

            layers.Add(layer);
        }

        ILossFunction<float> lossF = null;

        int lossFunctionChocie = Random.Shared.Next(0, 3);

        float huberLoss = (float)Random.Shared.NextDouble() * 0.75f + 0.05f;
        var huberP = new Params<float>(false);
        huberP.AddParameter(Params.Delta, huberLoss);

        switch (lossFunctionChocie)
        {
            case 0:
                layerDescription += $"[MS]";
                lossF = new MeanSquareFunction<float>();
                break;
            case 1:
                layerDescription += $"[H:{huberLoss}]";
                lossF = new HuberLossFunction<float>(huberP);
                break;
            case 2:
                layerDescription += $"[PH:{huberLoss}]";
                lossF = new PseudoHuberLossFunction<float>(huberP);
                break;
        }

        float LearningRate = (float)(Random.Shared.NextDouble() * 0.0008 + 0.0002);

        layerDescription += $".LR={LearningRate}\";";

        Console.WriteLine("Training: " + layerDescription);

        var randomNetwork =
        new NeuraNetwork<float>(
            layers.ToArray(),
            new DefaultForwardAlgorithm<float>(),
            new DefaultBackwardAlgorithm<float>(lossF),
            new GradientDescentOptimizer<float>(),
            new LayerAllocatedVariables<float>(layers.Count));

        writer.Write(layerDescription);



        ///READ ALL CSV LINES
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

        int epochs = 150 * 300;
        int bbb = 0;

        ///FIT

        for (int i = 0; i < epochs; i++)
        {
            //Console.WriteLine("Traning epoch: " + i);

            var batch = new List<(float[] inputs, float[] outputs)>();

            int digit = Random.Shared.Next(0, 10);

            int max = dic[digit].Length;

            for (int k = 0; k < 8; k++)
            {
                batch.Add(dic[digit][Random.Shared.Next(0, max)]);
            }

            randomNetwork.Fit([batch], LearningRate, epochs, x =>
            {
                //bbb++;
                //if (bbb > 450)
                //{
                //    writer.WriteLine(x);
                //    bbb = 0;
                //}
            });
            batch.Clear();

            if (i % 300 == 0)
            {
                // BEGIN TEST WITH TEST

                var lines2 = File.ReadLines(@"..\..\..\..\..\datasets\MNIST\mnist_test.csv").ToArray();

                //Console.WriteLine("Testing...");

                int total = 0;
                int wrong = 0;
                //foreach (var line in lines2)
                for (int z = 0; z < 4000; z++)
                {
                    var line = lines2[Random.Shared.Next(0, lines2.Length)];
                    total++;
                    var csvline = line.Split(',');
                    var outdigit = csvline.Take(1).Select(x => int.Parse(x)).First();

                    var output = new float[10];
                    output[outdigit] = 1.0f;

                    var image = csvline.Skip(1).Take(784).Select(x => (int.Parse(x) - 128) / 255.0f).ToArray();

                    var digits = randomNetwork.Predict(image);

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
                writer.Write(((total - wrong) / (float)total) + ";");
                //Console.WriteLine("Accuracy =" + ((total - wrong) / (float)total));
            }// END TEST WITH TEST
        }

        writer.WriteLine("");
    }
}

//for (int i = 0; i < 100; i++)
//    network.Fit(myEnum, 0.01f, 100);