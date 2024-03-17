# NeuraSharp
Extendible, customizable and scalable neural network library for c#  and .NET 8.0
CURRENTLY a WORK IN PROGRESS. IF YOU ARE INTERESTED FOLLOW/STAR

# Why
There are currently no good C# libraries, the existing ones are either no longer
maintained (Keras.NET, NeuralNetwork.NET), do not allow fine grained low level
control (ML.NET) or are just too simple or numerically unstable.

# The goal
With this library I want to set a starting point: I do not want to implement 
all existing methods, features or algorithms. that would be impossible for 1 
men. Instead I want to provide the most used ones, and to allow people to easily 
extend and experiment. Of Course I'm going to provide Sparse and Dense and 
Convolutional layers alongside ReLU and tanh, sigmoid activation functions 
and at least Adam optimization and stocastic gradient descent.

In example, for now I do not provide GPU support, but I want to make sure
adding it in future is possible with minor effort alongside CPU vectorization.

I do accept and encourage pull requests, even small ones in example for adding
new activation functions for neural layers. 

Another goal is to provide extensive documentation something missing in 
the realm of C# neural networks (not even ML.NET is documented properly).