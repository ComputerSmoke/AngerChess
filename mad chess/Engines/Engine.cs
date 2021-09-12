using System.Collections.Generic;
using System.Net.Http;

using static Tensorflow.Binding;
using static Tensorflow.KerasApi;
using Tensorflow;
using Tensorflow.Keras.Engine;
using NumSharp;
using Tensorflow.Keras.Layers;
using System;
using System.IO;
using System.Text.Json;

namespace MadChess
{
    abstract class Engine
    {
        protected string modelName;
        Functional model;
        protected Random rand;
        public Engine()
        {
            rand = new Random();
        }

        public void saveData(int[,,] bitmap, int won)
        {
            int id = rand.Next(0, 1000000000);
            string bitmapString = "";
            foreach(int bit in bitmap)
            {
                bitmapString += bit;
            }
            if (!Directory.Exists("./data")) Directory.CreateDirectory("./data");
            File.WriteAllText("./data/" + modelName + id + ".txt", bitmapString+"\n"+won);
        }

        public void build()
        {
            var layers = new LayersApi();
            // input layer
            var inputs = keras.Input(shape: (8, 8, 12), name: "bitmap");
            // convolutional layer
            var x = layers.Conv2D(1000, (3, 3), activation: "relu").Apply(inputs);
            x = layers.Conv2D(1000, (3, 3), activation: "relu").Apply(x);
            x = layers.Conv2D(1000, (3, 3), activation: "relu").Apply(x);
            x = layers.Flatten().Apply(x);
            x = layers.Dense(2000, activation: "relu").Apply(x);
            x = layers.Dense(1000, activation: "relu").Apply(x);
            x = layers.Dense(1000, activation: "relu").Apply(x);
            x = layers.Dense(1000, activation: "relu").Apply(x);
            x = layers.Dense(1000, activation: "relu").Apply(x);
            var outputs = layers.Dense(3, activation: "softmax").Apply(x);
            // build keras model
            model = keras.Model(inputs, outputs, name: modelName);
            model.summary();
            // compile keras model in tensorflow static graph
            model.compile(optimizer: keras.optimizers.RMSprop(1e-3f),
                loss: keras.losses.CategoricalCrossentropy(from_logits: true),
                metrics: new[] { "acc" });

            if (!File.Exists("./checkpoints")) Directory.CreateDirectory("./checkpoints");
            if (File.Exists("./checkpoints/" + modelName))
            {
                try
                {
                    model.load_weights(tf.train.latest_checkpoint("./checkpoints/" + modelName));
                }
                catch (Exception err)
                {
                    Console.WriteLine("Weights could not be loaded. Starting from randomized values.");
                }
            }


        }

        public double evaluate(int[,,] bitmap)
        {
            float[,,,] map4d = new float[1,bitmap.GetLength(0), bitmap.GetLength(1),bitmap.GetLength(2)];
            for(int i = 0; i < bitmap.GetLength(0); i++)
            {
                for(int j = 0; j < bitmap.GetLength(1); j++)
                {
                    for(int k = 0; k < bitmap.GetLength(2); k++)
                    {
                        map4d[0, i, j, k] = bitmap[i, j, k];
                    }
                }
            }
            Tensor tensor = tf.convert_to_tensor(map4d);
            Tensor preds = model.predict(tensor);
            return (float)((preds[0,0] - preds[0,1]) * (1 - preds[0,2]));
        }
        protected virtual double decisionFunction(double val)
        {
            return Math.Pow(20, val - 1);
        }
        protected int pickMoveIdx(double[] moveValues)
        {
            double val = rand.NextDouble() * moveValues[moveValues.GetLength(0) - 1];
            int max = moveValues.GetLength(0);
            int min = 0;
            int idx;
            while(min < max)
            {
                idx = (max + min) / 2;
                if (val < moveValues[idx]) {
                    if (idx == 0 || moveValues[idx - 1] < val) return idx;
                    max = idx;
                    continue;
                }
                min = idx;
            }
            return -1;
        }
        public void move(Board board)
        {
            List<Move> moves = board.getMoves();
            int n = moves.Count;
            if(n < 1)
            {
                throw new InvalidMoveException("No moves");
            }
            double[] moveValues = new double[n];
            for (int i = 0; i < n; i++)
            {
                int[,,] bitmap = board.getBitmap(moves[i]);
                if (i == 0) moveValues[i] = evaluate(bitmap);
                else moveValues[i] = moveValues[i - 1] + evaluate(bitmap);
            }
            int moveIdx = pickMoveIdx(moveValues);
            board.move(moves[moveIdx]);            
        }
    }
}
