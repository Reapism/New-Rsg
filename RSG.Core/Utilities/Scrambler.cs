﻿using RSG.Core.Interfaces;
using System;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Internal utilties for scrambling strings.
    /// </summary>
    public class Scrambler : IShuffle<char>
    {
        public void Shuffle<T>(T[] array, Random random)
        {
            KnuthShuffle(array, random);
        }

        /// <summary>
        /// Performs a Knuth Shuffle on the <typeparamref name="T"/> array.
        /// </summary>
        /// <typeparam name="T">A Type array.</typeparam>
        /// <param name="array">An array of type <typeparamref name="T"/>.</param>
        /// <param name="random">The <see cref="Random"/> provider.</param>
        private void KnuthShuffle<T>(T[] array, Random random)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var j = random.Next(i, array.Length); // Don't select from the entire array on subsequent loops
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

    }
}
