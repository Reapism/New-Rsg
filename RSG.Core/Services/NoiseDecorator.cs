﻿using RSG.Core.Models;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    internal class NoiseDecorator
    {
        public static async void Decorate(Words words, int lowPercentage, int highPercentage)
        {
            var result = ValidatePercentageRange(lowPercentage, highPercentage);

            switch (result)
            {
                case 0:
                    await Task.Factory.StartNew(() =>
                    {
                        DecorateHelper(words, lowPercentage, highPercentage);
                    });
                    return;
                case 1:
                    throw new ArgumentException("The lower percentage is out of range. Should be 0 <= x < 100");
                case 2:
                    throw new ArgumentException("The higher percentage is out of range. Should be 0 <= x <= 100");
                case 3:
                    throw new ArgumentException("The lower percentage cannot be larger than the higher percentage.");
                default:
                    throw new Exception("The error code returned is not contained here.");
            }
        }

        private static void DecorateHelper(in Words words, int lowPercentage, int highPercentage)
        {
            var randomPercentage = RandomProvider.Random.Next(lowPercentage, highPercentage);
            BigInteger partioned = BigInteger.DivRem(words.Count(), BigInteger.Parse(randomPercentage.ToString()), out BigInteger remainder);

            foreach (System.Collections.Concurrent.ConcurrentQueue<Interfaces.IGeneratedWord> partitionedDictionary in words.PartitionedWords)
            {
                foreach (Interfaces.IGeneratedWord noisyWord in partitionedDictionary)
                {
                }
            }
        }

        private static int ValidatePercentageRange(int lowPercentage, int highPercentage)
        {
            if (lowPercentage < 0 && lowPercentage >= 100)
                return 1;
            if (highPercentage < 0 && highPercentage > 100)
                return 2;
            if (lowPercentage > highPercentage)
                return 3;

            return 0;
        }
    }
}
