﻿using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// The base class for a Result
    /// </summary>
    public abstract class ResultBase : IResult
    {
        private static IResult emptyResult;

        static ResultBase()
        {
            emptyResult = new Result
            {
                EndTime = DateTime.UnixEpoch,
                StartTime = DateTime.UnixEpoch,
                Iterations = BigInteger.MinusOne,
                RandomizationType = Enums.RandomizationType.Pseudorandom
            };

        }

        /// <summary>
        /// Gets or sets the randomization type used during this generation instance.
        /// </summary>
        public RandomizationType RandomizationType { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations for this generation.
        /// </summary>
        public BigInteger Iterations { get; set; }

        /// <summary>
        /// Gets or sets the start time of this generation.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of this generation.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Returns an empty representation of <see cref="IResult"/>.
        /// </summary>
        /// <returns>Returns an empty <see cref="IResult"/>.</returns>
        public static IResult Empty()
        {
            return emptyResult;
        }
    }
}