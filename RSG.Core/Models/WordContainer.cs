﻿using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a collection of generated words.
    /// </summary>
    public class WordContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordContainer"/> class
        /// and if it's noisy.
        /// </summary>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        public WordContainer(bool isNoisy)
        {
            IsNoisy = isNoisy;

            // Queue is number of partitions, Dictionary is number of words K: index, V: IGeneratedWord
            PartitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
        }

        /// <summary>
        /// Gets a value that maps a partitioned collection of <see cref="IGeneratedWord"/>(s).
        /// </summary>
        public ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>> PartitionedWords { get; internal set; }

        /// <summary>
        /// Gets the number of total words stored in this instance.
        /// </summary>
        public BigInteger Count => GetCount();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WordContainer"/>
        /// instance contains noise.
        /// </summary>
        public bool IsNoisy { get; }

        /// <summary>
        /// Clears all of the words stored in this instance.
        /// </summary>
        public void Clear()
        {
            foreach (var dictionary in PartitionedWords)
            {
                dictionary.Clear();
            }
        }

        /// <summary>
        /// Returns a specific partition of words given the partition index.
        /// </summary>
        /// <param name="partitionIndex">The index of the parent collection.</param>
        /// <returns>A specific partition </returns>
        public ConcurrentDictionary<int, IGeneratedWord> GetWordsAtIndex(int partitionIndex)
        {
            var count = PartitionedWords.Count();
            var emptyQueue = new ConcurrentDictionary<int, IGeneratedWord>();

            if (partitionIndex < 0 || partitionIndex >= count)
            {
                return emptyQueue;
            }

            return PartitionedWords.ElementAt(partitionIndex);
        }

        public bool Contains(string word)
        {
            foreach (var kvp in PartitionedWords)
            {
                if (kvp.Any(e => e.Value.Word.Equals(word, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }

            return false;
        }

        public int GetNumberOfOccurencesFor(string word)
        {
            var counter = 0;
            foreach (var kvp in PartitionedWords)
            {
                counter += kvp.Count(e => e.Value.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            }

            return counter;
        }

        /// <summary>
        /// Gets the new count of all the words stored.
        /// </summary>
        private BigInteger GetCount()
        {
            var count = BigInteger.Zero;

            foreach (var c in PartitionedWords)
            {
                count += c.Count.ToBigInteger();
            }

            return count;
        }
    }
}
